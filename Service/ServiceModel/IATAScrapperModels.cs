using Service.ServiceModel.PublicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceModel.IATAScrapperModels
{
    public class IATAResponse
    {
        public Error Error { get; set; }
        public bool Success { get; set; }
        public List<Airport> Airports { get; set; }
    }
    public class Airport
    {
        public string CityName { get; set; }
        public string AirportName { get; set; }
        public string CityCode { get; set; }
        public string AirportCode { get; set; }
    }
}
