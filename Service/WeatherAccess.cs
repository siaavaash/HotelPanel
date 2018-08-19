using Service.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class WeatherAccess
    {
        public static Weather.RootObject Get(string City)
        {
            HttpClient client = new HttpClient();
            var result = client.PostAsync("https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22"+City+"%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys", null).Result;
            var jsonResult = result.Content.ReadAsStringAsync().Result;
            var ConvertResult = Newtonsoft.Json.JsonConvert.DeserializeObject<Weather.RootObject>(jsonResult);
            return ConvertResult;
        }
    }
}
