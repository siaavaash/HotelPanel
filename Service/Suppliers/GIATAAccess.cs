using Service.ServiceModel.GIATAModels;
using Service.ServiceModel.PublicModels;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Service.Suppliers
{
    public class GIATAAccess
    {
        // GIATA web service url
        private const string serviceUrl = "http://multicodes.giatamedia.com/webservice/rest/1.latest/properties/";

        /// <summary>
        /// Get Properties by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultDataModel GetPropertiesById(long id)
        {
            try
            {
                var (statusCode, data) = GetXML(serviceUrl + id);

                if (statusCode == HttpStatusCode.OK)
                {
                    var propertySerializer = new XmlSerializer(typeof(Properties));
                    return new ResultDataModel
                    {
                        Success = true,
                        Model = (Properties)propertySerializer.Deserialize(data),
                    };
                }
                var serializer = new XmlSerializer(typeof(ServiceModel.GIATAModels.Error));
                var model = (ServiceModel.GIATAModels.Error)serializer.Deserialize(data);
                if (statusCode == HttpStatusCode.MovedPermanently)
                    return new ResultDataModel
                    {
                        Success = true,
                        Model = model,
                    };
                return new ResultDataModel
                {
                    Success = true,
                    Error = new ServiceModel.PublicModels.Error
                    {
                        Code = model.Code,
                        Text = model.Description.Text,
                    },
                };
            }
            catch (HttpRequestException httpEx)
            {
                return new ResultDataModel
                {
                    Success = false,
                    Error = new ServiceModel.PublicModels.Error
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
                    Error = new ServiceModel.PublicModels.Error
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
        public (HttpStatusCode statusCode, Stream data) GetXML(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", "bmV2aWxsZXxpdG91cnMubm86cDZNUkVLcHg =");
                    var response = client.GetAsync(url).Result;
                    return (response.StatusCode, response.Content.ReadAsStreamAsync().Result);
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
