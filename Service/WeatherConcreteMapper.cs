using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class WeatherConcreteMapper
    {
        public static ServiceModel.Weather.RootObject Avail(string CityName)
        {
            var WeatherAvail = Service.WeatherAccess.Get(CityName);
            return WeatherAvail;
        }
    }
}
