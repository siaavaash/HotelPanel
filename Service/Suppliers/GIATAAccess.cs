using Newtonsoft.Json;
using Service.ServiceModel.GIATAModels;
using Service.ServiceModel.PublicModels;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Xml;
using System.Xml.Serialization;

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
        public ResultDataModel GetPropertiesById(long id)
        {
            try
            {
                HttpStatusCode statusCode = GetXML(serviceUrl + id, out Stream response);

                if (statusCode == HttpStatusCode.OK)
                {
                    var propertySerializer = new XmlSerializer(typeof(properties));
                    return new ResultDataModel
                    {
                        Success = true,
                        Model = (properties)propertySerializer.Deserialize(response),
                    };
                }
                var serializer = new XmlSerializer(typeof(error));
                var model = (error)serializer.Deserialize(response);
                if (statusCode == HttpStatusCode.MovedPermanently)
                    return new ResultDataModel
                    {
                        Success = true,
                        Model = model,
                    };
                return new ResultDataModel
                {
                    Success = false,
                    Error = new Error
                    {
                        Code = model.code.ToString(),
                        Text = model.description,
                    },
                };
            }
            catch (HttpRequestException httpEx)
            {
                return new ResultDataModel
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
                return new ResultDataModel
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
        public HttpStatusCode GetXML(string url, out Stream data)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", "bmV2aWxsZXxpdG91cnMubm86cDZNUkVLcHg =");
                    var response = client.GetAsync(url).Result;
                    data = response.Content.ReadAsStreamAsync().Result;
                    return response.StatusCode;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
