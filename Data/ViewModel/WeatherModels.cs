using Data.PublicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel
{
    public class WeatherModels
    {
        public class Forecast
        {
            public class ForecastDays
            {
                public int Code { get; set; }
                public string Date { get; set; }
                public string Day { get; set; }
                public string High { get; set; }
                public string Low { get; set; }
                public string Text { get; set; }
            }
            public class Root
            {
                public string Wind { get; set; }
                public string Sunrise { get; set; }
                public string Pressure { get; set; }    
                public LocationModels.Google Google { get; set; }
                public List<ForecastDays> Forecast { get; set; }
            }
        }
    }
}
