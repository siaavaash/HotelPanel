using Service.ServiceModel.GIATAModels;
using Service.ServiceModel.PublicModels;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Service.Suppliers
{
    public class GIATAAccess
    {
        private static Lazy<HttpClient> client = new Lazy<HttpClient>(() =>
        {
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(5);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", "bmV2aWxsZXxpdG91cnMubm86cDZNUkVLcHg =");
            return client;
        });

        // GIATA web service url
        private const string serviceUrl = "http://multicodes.giatamedia.com/webservice/rest/1.latest/properties/";

        public static string GetUrlByParameters(string version, Method method, string parameter) => $"http://multicodes.giatamedia.com/webservice/rest/{version}/{method}/{parameter}";
        public static string GetPropertiesUrlWithFilter(string version, Filter filter, string parameter) => $"http://multicodes.giatamedia.com/webservice/rest/{version}/properties/{filter}/{parameter}";

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

        public async Task<byte[]> GetHotelDataByIDAsync(int id, string version)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = GetUrlByParameters(version, Method.properties, id.ToString());
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", "bmV2aWxsZXxpdG91cnMubm86cDZNUkVLcHg =");
                    var response = await client.GetAsync(url);
                    return await response.Content.ReadAsByteArrayAsync();
                }
            }
            catch (System.Exception)
            {

                try
                {
                    using (var client = new HttpClient())
                    {
                        var url = GetUrlByParameters(version, Method.properties, id.ToString());
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", "bmV2aWxsZXxpdG91cnMubm86cDZNUkVLcHg =");
                        var response = await client.GetAsync(url);
                        return await response.Content.ReadAsByteArrayAsync();
                    }
                }
                catch (System.Exception)
                {
                    return null;
                }
            }
        }

        public string[] GetAllProvider(string version, Filter filter)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(providers));
            var response = GetXML(GetUrlByParameters(version, Method.providers, Enum.GetName(typeof(Filter), filter)));
            if (response.statusCode == HttpStatusCode.OK)
                return ((providers)serializer.Deserialize(response.data)).provider.Select(x => x.providerCode).ToArray();
            return null;
        }
        public async Task<byte[]> GetXMLAsync(string url)
        {
            try
            {
                var response = await client.Value.GetAsync(url);
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsByteArrayAsync();
                return null;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<GIATAFile> GetHotelsByFilterAsync(string version, string code, Filter filter, SemaphoreSlim semaphore)
        {
            await semaphore.WaitAsync();
            try
            {
                var url = GetPropertiesUrlWithFilter(version, filter, code);
                var response = await GetXMLAsync(url);
                if (response != null)
                {
                    return new GIATAFile
                    {
                        Contents = response,
                        Extention = ".xml",
                        Name = code
                    };
                }
                return null;
            }
            catch (System.Exception)
            {

                throw;
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
