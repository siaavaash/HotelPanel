using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel
{
    public class AirportModels
    {
        public class SearchAirport
        {
            public long? AirportID { get; set; }
            public long? LocationID { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
        }        
    }
}
