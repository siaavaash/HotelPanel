using System;
using System.Collections.Generic;

namespace Service.ServiceModel.GIATAModels
{

    #region GIATA Response Model
    public class Properties
    {
        public string xmlnsxsi { get; set; }
        public string xmlnsxlink { get; set; }
        public Property property { get; set; }
    }

    public class Property
    {
        public string giataId { get; set; }
        public DateTime lastUpdate { get; set; }
        public string name { get; set; }
        public Alternativenames alternativeNames { get; set; }
        public City city { get; set; }
        public Destination destination { get; set; }
        public string country { get; set; }
        public string category { get; set; }
        public Ratings ratings { get; set; }
        public Airports airports { get; set; }
        public Addresses addresses { get; set; }
        public Phones phones { get; set; }
        public Emails emails { get; set; }
        public Urls urls { get; set; }
        public Geocodes geoCodes { get; set; }
        public Propertycodes propertyCodes { get; set; }
        public Chains chains { get; set; }
        public Ghgml ghgml { get; set; }
    }

    public class Alternativenames
    {
        public Alternativename[] alternativeName { get; set; }
    }

    public class Alternativename
    {
        public string alternativeNameType { get; set; }
        public string text { get; set; }
        public string effectiveDate { get; set; }
    }

    public class City
    {
        public string cityId { get; set; }
        public string text { get; set; }
    }

    public class Destination
    {
        public string destinationId { get; set; }
        public string text { get; set; }
    }

    public class Ratings
    {
        public Rating rating { get; set; }
    }

    public class Rating
    {
        public string value { get; set; }
        public string isDefault { get; set; }
    }

    public class Airports
    {
        public Airport[] airport { get; set; }
    }

    public class Airport
    {
        public string iata { get; set; }
    }

    public class Addresses
    {
        public Address address { get; set; }
    }

    public class Address
    {
        public Addressline[] addressLine { get; set; }
        public string street { get; set; }
        public string cityName { get; set; }
        public string country { get; set; }
    }

    public class Addressline
    {
        public string addressLineNumber { get; set; }
        public string text { get; set; }
    }

    public class Phones
    {
        public Phone[] phone { get; set; }
    }

    public class Phone
    {
        public string tech { get; set; }
        public string text { get; set; }
    }

    public class Emails
    {
        public string email { get; set; }
    }

    public class Urls
    {
        public string url { get; set; }
    }

    public class Geocodes
    {
        public Geocode geoCode { get; set; }
    }

    public class Geocode
    {
        public string accuracy { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    }

    public class Propertycodes
    {
        public Provider[] provider { get; set; }
    }

    public class Provider
    {
        public string providerCode { get; set; }
        public string providerType { get; set; }
        public Code code { get; set; }
    }

    public class Code
    {
        public string status { get; set; }
        public long value { get; set; }
    }

    public class Chains
    {
        public Chain chain { get; set; }
    }

    public class Chain
    {
        public string chainId { get; set; }
        public string chainName { get; set; }
    }

    public class Ghgml
    {
        public string xlinkhref { get; set; }
    }
    #endregion

    /// <summary>
    /// GIATA Database Transfer Model
    /// </summary>
    public class GIATADbTransferModel
    {
        public long CountryId { get; set; }
        public long AccommodationId { get; set; }
        public long ChainId { get; set; }
        public string AirportCode { get; set; }
        public string Name { get; set; }
        public string Rating { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime LastUpdate { get; set; }
        public string CountryCode { get; set; }
        public string CityName { get; set; }
        public long CityId { get; set; }
        public string Xml { get; set; }
        public string CountryName { get; set; }
        public string DestinationId { get; set; }
        public string DestinationCode { get; set; }
        public string Email { get; set; }
        public string Category { get; set; }
        public string Url { get; set; }
        public List<AlternativeName> AlternativeNames { get; set; }
        public List<Airport> Airports { get; set; }
        public List<AccommodationSupplier> Suppliers { get; set; }
    }
    public class AlternativeName
    {
        public string Name { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string Type { get; set; }
    }
    public class AccommodationAirport
    {
        public string AirportCode { get; set; }
    }
    public class AccommodationSupplier
    {
        public string Code { get; set; }
        public bool Active { get; set; }
        public string ProviderType { get; set; }
        public string ProviderCode { get; set; }
        public string ProviderValue { get; set; }
    }
}
