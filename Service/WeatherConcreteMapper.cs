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
        public static WeatherModels.Forecast.Root Avail(string CityName)
        {
            var WeatherAvail = Service.WeatherAccess.Get(CityName);
            WeatherModels.Forecast.Root Result = new WeatherModels.Forecast.Root();
            return Result;
        }
    }
}
