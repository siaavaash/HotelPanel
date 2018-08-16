using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel
{
    public class AccommodationModels
    {
        public class ListNameAccommodation
        {
            public long AccommodationlID { get; set; }
            public string Name { get; set; }
            public string CityName { get; set; }
            public string Country { get; set; }
            public Nullable<System.DateTime> lastUpdate { get; set; }
        }
    }
}
