using System.Collections.Generic;

namespace Service.ServiceModel.DOTWModels
{
    public class SearchHotelByCityViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string CityCode { get; set; }
    }

    public class MyFile
    {
        public string Name { get; set; }
        public string Extention { get; set; }
        public byte[] Contents { get; set; }
    }

    public class CitiesByCountryModel
    {
        public string CountryName { get; set; }
        public int CountryCode { get; set; }
        public Suppliers.DOTWModels.CitiesModel.resultCitiesCity[] Cities { get; set; }

    }

    public class CityModel
    {
        public string Name { get; set; }
        public int Code { get; set; }
    }
}
