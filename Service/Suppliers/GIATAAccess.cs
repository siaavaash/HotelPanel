using Newtonsoft.Json;
using Service.ServiceModel.GIATAModels;
using Service.ServiceModel.PublicModels;
using System.Net.Http;
using System.Xml;

namespace Service.Suppliers
{
    public class GIATAAccess
    {
        // GIATA web service url
        private const string serviceUrl = "http://multicodes.giatamedia.com/webservice/rest/1.0/properties/";

        /// <summary>
        /// Get Properties by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultDataModel<Properties> GetPropertiesById(long id)
        {
            try
            {
                var xmlResponse = GetXML(serviceUrl + id);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlResponse);

                string jsonResponse = JsonConvert.SerializeXmlNode(doc);
                return new ResultDataModel<Properties>
                {
                    Success = true,
                    Model = JsonConvert.DeserializeObject<Properties>(jsonResponse)
                };
            }
            catch (HttpRequestException httpEx)
            {
                return new ResultDataModel<Properties>
                {
                    Success = false,
                    Error = new Error
                    {
                        Text = httpEx.Message,
                        IsApiError = true,
                    },
                };
            }
            catch (System.Exception ex)
            {
                return new ResultDataModel<Properties>
                {
                    Success = false,
                    Error = new Error
                    {
                        Text = ex.Message
                    },
                };
            }
        }

        /// <summary>
        /// Return xml response in string
        /// </summary>
        /// <param name="url">server url</param>
        /// <returns></returns>
        public string GetXML(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", "bmV2aWxsZXxpdG91cnMubm86cDZNUkVLcHg =");
                    var response = client.GetAsync(url).Result;
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        return response.Content.ReadAsStringAsync().Result;
                    throw new HttpRequestException();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
