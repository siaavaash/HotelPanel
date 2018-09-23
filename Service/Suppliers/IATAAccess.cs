using Common;
using Service.ServiceModel.IATACodeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Service.Suppliers
{
    public class IATAScrapper
    {
        private static AutoResetEvent resetEvent = new AutoResetEvent(false);
        private const string IATAUrl = "https://www.iata.org/publications/Pages/code-search.aspx";
        public static IATAResponse GetIATALocation(IATASearchBy searchBy, string parameter)
        {
            var result = new IATAResponse();
            try
            {
                var thread = new Thread(() =>
                {
                    try
                    {
                        using (var browser = new WebBrowser())
                        {
                            browser.AllowNavigation = true;
                            browser.Navigate(IATAUrl);
                            browser.ScriptErrorsSuppressed = false;
                            browser.DocumentCompleted += documentCompletedEventHandler;
                            void documentCompletedEventHandler(object sender, WebBrowserDocumentCompletedEventArgs e)
                            {
                                if (browser.ReadyState == WebBrowserReadyState.Complete || browser.ReadyState == WebBrowserReadyState.Interactive)
                                {
                                    browser.DocumentCompleted -= documentCompletedEventHandler;
                                    var options = browser.Document.GetElementsByTagName("select")[0].Document.GetElementsByTagName("option");
                                    foreach (HtmlElement option in options)
                                    {
                                        option.SetAttribute("selected", string.Empty);
                                        if (option.GetAttribute("value") == searchBy.ToString())
                                            option.SetAttribute("selected", "selected");
                                    }
                                    browser.Document.GetElementById("ctl00_SPWebPartManager1_g_e3b09024_878e_4522_bd47_acfefd1000b0_ctl00_txtSearchCriteria").SetAttribute("value", parameter);
                                    browser.Document.GetElementById("ctl00_SPWebPartManager1_g_e3b09024_878e_4522_bd47_acfefd1000b0_ctl00_butSearch").InvokeMember("click");
                                }
                            };
                            var counter = 0;
                            while (browser.ReadyState != WebBrowserReadyState.Complete && counter++ < 10)
                                Application.DoEvents();
                            HtmlElement dataTable = null;
                            var baseTime = DateTime.Now;
                            while (dataTable == null && DateTime.Now - baseTime < new TimeSpan(50000000))
                            {
                                Application.DoEvents();
                                dataTable = browser.Document?.GetElementById("ctl00_SPWebPartManager1_g_e3b09024_878e_4522_bd47_acfefd1000b0_ctl00_panResults")?.GetElementsByTagName("table")[0];
                            }
                            if (dataTable != null)
                            {
                                result.Data = new List<IATAData>();
                                result.Success = true;
                                foreach (HtmlElement tr in dataTable.GetElementsByTagName("tbody")[0].Children)
                                {
                                    result.Data.Add(new IATAData
                                    {
                                        CityName = tr.Children[0].InnerHtml,
                                        CityCode = tr.Children[1].InnerHtml,
                                        AirportName = tr.Children[2].InnerHtml,
                                        AirportCode = tr.Children[3].InnerHtml,
                                    });
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
                if (thread != null) thread.Abort();
                return result;
            }
            catch (Exception ex)
            {
                return new IATAResponse
                {
                    Error = new ServiceModel.PublicModels.Error
                    {
                        Text = ex.Message,
                    },
                    Success = false,
                };
            }
        }
    }

    /// <summary>
    /// gcmap.com
    /// </summary>
    public class GCMAPCrawler
    {
        private const string BaseUrl = "http://www.gcmap.com/airport/";
        private static AutoResetEvent resetEvent = new AutoResetEvent(false);
        public GCMAPResponse GetGCMAPData(string parameter)
        {
            try
            {
                var url = BaseUrl + parameter;
                var result = new GCMAPResponse();
                var thread = new Thread(() =>
                {
                    try
                    {
                        using (var browser = new WebBrowser())
                        {
                            browser.AllowNavigation = true;
                            browser.ScriptErrorsSuppressed = false;
                            browser.DocumentCompleted += documentCompletedEventHandler;
                            browser.Navigate(url);
                            void documentCompletedEventHandler(object sender, WebBrowserDocumentCompletedEventArgs e)
                            {
                                if (((WebBrowser)sender).ReadyState == WebBrowserReadyState.Complete)
                                {
                                    browser.DocumentCompleted -= documentCompletedEventHandler;
                                    HtmlDocument doc = ((WebBrowser)sender).Document;
                                    var trs = doc.GetElementById("mid")?.GetElementsByTagName("table")[0]?.GetElementsByTagName("tbody")[0].GetElementsByTagName("table")[0].GetElementsByTagName("tbody")[0].Children;
                                    if (trs != null)
                                    {
                                        result.Data = new GCMAPData();
                                        foreach (HtmlElement tr in trs)
                                        {
                                            switch (tr.FirstChild.InnerText)
                                            {
                                                case "City:":
                                                    result.Data.City = tr.Children[2].Children[0].InnerText;
                                                    result.Data.Country = tr.Children[2].Children[2].InnerText;
                                                    break;
                                                case "IATA:":
                                                    result.Data.IATACode = tr.Children[1].GetElementsByTagName("a")[0].InnerText;
                                                    break;
                                                case "Type:":
                                                    result.Data.Type = tr.Children[1].InnerText;
                                                    break;
                                                case "Latitude:":
                                                    result.Data.Latitude = tr.Children[1].GetElementsByTagName("abbr")[0].GetAttribute("title");
                                                    break;
                                                case "Longitude:":
                                                    result.Data.Longitude = tr.Children[1].GetElementsByTagName("abbr")[0].GetAttribute("title");
                                                    break;
                                                case "Name:":
                                                    result.Data.Name = tr.Children[1].InnerText;
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                        result.Success = true;
                                    }
                                    else
                                    {
                                        result.Error = new ServiceModel.PublicModels.Error
                                        {
                                            Text = "The Parameter is invalid."
                                        };
                                        result.Success = false;
                                    }
                                }
                            }
                            var baseTime = DateTime.Now;
                            while (browser.ReadyState != WebBrowserReadyState.Complete && DateTime.Now - baseTime < new TimeSpan(0, 0, 30))
                                Application.DoEvents();
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
                if (thread != null) thread.Abort();
                return result;
            }
            catch (Exception ex)
            {
                return new GCMAPResponse
                {
                    Error = new ServiceModel.PublicModels.Error
                    {
                        Text = ex.Message,
                    },
                    Success = false,
                };
            }
        }
    }

    /// <summary>
    /// iatacodes.org
    /// </summary>
    public class IataCodeAccess
    {
        private const string url = "https://iatacodes.org/api/v6/airports.xml?api_key=f0843854-379f-481c-87aa-3182e174f3f5";
        public static IataCodeResponse GetIataCodes()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = client.GetAsync(url).Result;
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var xml = response.Content.ReadAsStreamAsync().Result;
                        var xmlSerializer = new XmlSerializer(typeof(root));
                        var model = (root)xmlSerializer.Deserialize(xml);
                        if (model != null)
                        {
                            var result = new IataCodeResponse
                            {
                                Success = true,
                                Data = model.response.AsParallel().Select(x => new IataCodeData
                                {
                                    Code = x.code,
                                    Name = x.name
                                }).ToList()
                            };
                            return result;
                        }
                        return new IataCodeResponse
                        {
                            Success = false,
                            Error = new ServiceModel.PublicModels.Error
                            {
                                Text = "Serialize Xml failed.",
                            }
                        };
                    }
                    return new IataCodeResponse
                    {
                        Success = false,
                        Error = new ServiceModel.PublicModels.Error
                        {
                            Text = "Server Response is invalid.",
                        }
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
