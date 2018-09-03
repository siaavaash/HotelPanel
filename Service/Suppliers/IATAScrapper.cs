using Common;
using Service.ServiceModel.IATAScrapperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Service.Suppliers
{
    public class IATAScrapper
    {
        private const string IATAUrl = "https://www.iata.org/publications/Pages/code-search.aspx";
        public IATAResponse GetIATAData(IATASearchBy searchBy, string parameter)
        {
            var result = new IATAResponse();
            try
            {
                using (var browser = new WebBrowser())
                {
                    browser.Navigate(IATAUrl);
                    var options = browser.Document.GetElementsByTagName("select")[0].Document.GetElementsByTagName("option");
                    foreach (HtmlElement option in options)
                    {
                        option.SetAttribute("selected", string.Empty);
                        if (option.GetAttribute("value") == searchBy.ToString())
                            option.SetAttribute("selected", "selected");
                    }
                    browser.Document.GetElementById("ctl00_SPWebPartManager1_g_e3b09024_878e_4522_bd47_acfefd1000b0_ctl00_txtSearchCriteria").SetAttribute("value", parameter);
                    browser.Document.GetElementById("ctl00_SPWebPartManager1_g_e3b09024_878e_4522_bd47_acfefd1000b0_ctl00_butSearch").InvokeMember("click");
                    Thread.Sleep(new TimeSpan(0, 0, 10));
                    HtmlElement dataTable = null;
                    foreach (HtmlElement table in browser.Document.GetElementsByTagName("table"))
                        if (table.GetAttribute("class") == "datatable")
                            dataTable = table;
                    if (dataTable != null)
                    {
                        result.Airports = new List<Airport>();
                        result.Success = true;
                        foreach (HtmlElement tr in dataTable.GetElementsByTagName("tbody")[0].Children)
                        {
                            result.Airports.Add(new Airport
                            {
                                CityName = tr.Children[0].InnerHtml,
                                CityCode = tr.Children[1].InnerHtml,
                                AirportName = tr.Children[2].InnerHtml,
                                AirportCode = tr.Children[3].InnerHtml,
                            });
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return new IATAResponse
                {
                    Error = new ServiceModel.PublicModels.Error
                    {
                        Text = ex.Message
                    },
                    Success = false,
                };
            }
        }
    }
}
