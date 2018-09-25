using Common;
using HtmlAgilityPack;
using MoreLinq;
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
                            browser.Navigate(IATAUrl);
                            browser.ScriptErrorsSuppressed = true;
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
                            HtmlElement resultNode = null;
                            var baseTime = DateTime.Now;
                            while (resultNode == null && DateTime.Now - baseTime < new TimeSpan(50000000))
                            {
                                Application.DoEvents();
                                resultNode = browser.Document?.GetElementById("ctl00_SPWebPartManager1_g_e3b09024_878e_4522_bd47_acfefd1000b0_ctl00_panResults");
                            }
                            if (resultNode != null)
                            {
                                var dataTable = resultNode?.GetElementsByTagName("table");
                                if (dataTable.Count <= 0) result = new IATAResponse { Success = true, Data = new List<IATAData>() };
                                else
                                {
                                    result.Data = new List<IATAData>();
                                    result.Success = true;
                                    foreach (HtmlElement tr in dataTable[0].GetElementsByTagName("tbody")?[0]?.Children)
                                    {
                                        result.Data.Add(new IATAData
                                        {
                                            CityName = tr?.Children[0]?.InnerHtml,
                                            CityCode = tr?.Children[1]?.InnerHtml,
                                            AirportName = tr?.Children[2]?.InnerHtml,
                                            AirportCode = tr?.Children[3]?.InnerHtml,
                                        });
                                    }
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
        public GCMAPResponse GetGCMAPData(string parameter)
        {
            try
            {
                var web = new HtmlWeb();
                var doc = web.Load(BaseUrl + parameter);
                var nearby = "";
                var old = doc.DocumentNode.Descendants("td")?.FirstOrDefault(x => x.InnerText == "Old (Alt.):")?.ParentNode.ChildNodes[1].InnerText;
                var timezone = doc.DocumentNode.Descendants("td")?.FirstOrDefault(x => x.InnerText == "Time Zone:")?.ParentNode.ChildNodes[1].InnerText;
                doc.DocumentNode.Descendants("td")?.FirstOrDefault(x => x.InnerText == "Nearby:")?.ParentNode.Descendants("a")?.ForEach(x => nearby += $"{x.InnerText} ");
                return new GCMAPResponse
                {
                    Success = true,
                    Data = new GCMAPData
                    {
                        City = doc.DocumentNode.Descendants().FirstOrDefault(x => x.Attributes["class"]?.Value == "locality")?.InnerText,
                        Country = doc.DocumentNode.Descendants().FirstOrDefault(x => x.Attributes["class"]?.Value == "country-name")?.InnerText,
                        IATACode = doc.DocumentNode.Descendants().Where(x => x.Attributes["class"]?.Value == "nickname url").ToArray()[1]?.InnerText ?? doc.DocumentNode.Descendants().FirstOrDefault(x => x.Attributes["class"]?.Value == "nickname url")?.InnerText,
                        Type = doc.DocumentNode.Descendants().FirstOrDefault(x => x.Attributes["class"]?.Value == "note")?.InnerText,
                        Latitude = doc.DocumentNode.Descendants().FirstOrDefault(x => x.Attributes["class"]?.Value == "latitude")?.Attributes["title"]?.Value,
                        Longitude = doc.DocumentNode.Descendants().FirstOrDefault(x => x.Attributes["class"]?.Value == "longitude")?.Attributes["title"]?.Value,
                        Name = doc.DocumentNode.Descendants().FirstOrDefault(x => x.Attributes["class"]?.Value == "fn org")?.InnerText,
                        Nearby = nearby,
                        OldName = old,
                        TimeZone = timezone,
                    }
                };
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
