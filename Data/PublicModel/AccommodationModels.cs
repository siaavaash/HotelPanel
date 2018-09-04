using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.PublicModel
{
    public class AccommodationModels
    {
        public class SearchReport
        {
            public long LocationID { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            public long Count { get; set; }
            public long Percent { get; set; }
        }
    }
}
