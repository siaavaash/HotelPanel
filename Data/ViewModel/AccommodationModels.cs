using Data.DataModel;
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
            public long AccommodationID { get; set; }
            public string Name { get; set; }
            public string CityName { get; set; }
            public string Country { get; set; }
            public Nullable<System.DateTime> lastUpdate { get; set; }
        }
        public class SearchAccommodation
        {
            public long? AccommodationID { get; set; }
            public string Name { get; set; }
            public string CityName { get; set; }
            public string Country { get; set; }
            public long? From { get; set; }
            public long? To { get; set; }
        }
        public class AccommodationFacility
        {
            public long AccommodationID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public List<Facility> Facilities { get; set; }
        }
    }
}
