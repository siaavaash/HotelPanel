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
                public string Code { get; set; }
                public DateTime Date { get; set; }
                public string Day { get; set; }
                public decimal High { get; set; }
                public decimal Low { get; set; }
                public string Text { get; set; }
            }
            public class Root
            {
                public int Wind { get; set; }
                public DateTime Sunrise { get; set; }
                public string Pressure { get; set; }    
                public LocationModels.Google Google { get; set; }
                public List<ForecastDays> Forecast { get; set; }
            }
        }
    }
}
