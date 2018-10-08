using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Service.ServiceModel.GIATAModels
{
    public enum Version
    {
        old = 1,
        latest = 2,
    }


    #region GIATA Response Model

    public interface IResponse { }

    [XmlRoot(ElementName = "alternativeName")]
    public class AlternativeName
    {
        [XmlAttribute(AttributeName = "alternativeNameType")]
        public string AlternativeNameType { get; set; }
        [XmlText]
        public string Text { get; set; }
        [XmlAttribute(AttributeName = "effectiveDate")]
        public string EffectiveDate { get; set; }
    }

    [XmlRoot(ElementName = "alternativeNames")]
    public class AlternativeNames
    {
        [XmlElement(ElementName = "alternativeName")]
        public List<AlternativeName> AlternativeName { get; set; }
    }

    [XmlRoot(ElementName = "city")]
    public class City
    {
        [XmlAttribute(AttributeName = "cityId")]
        public string CityId { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "destination")]
    public class Destination
    {
        [XmlAttribute(AttributeName = "destinationId")]
        public string DestinationId { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "rating")]
    public class Rating
    {
        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
        [XmlAttribute(AttributeName = "isDefault")]
        public string IsDefault { get; set; }
    }

    [XmlRoot(ElementName = "ratings")]
    public class Ratings
    {
        [XmlElement(ElementName = "rating")]
        public Rating Rating { get; set; }
    }

    [XmlRoot(ElementName = "airport")]
    public class Airport
    {
        [XmlAttribute(AttributeName = "iata")]
        public string Iata { get; set; }
    }

    [XmlRoot(ElementName = "airports")]
    public class Airports
    {
        [XmlElement(ElementName = "airport")]
        public List<Airport> Airport { get; set; }
    }

    [XmlRoot(ElementName = "addressLine")]
    public class AddressLine
    {
        [XmlAttribute(AttributeName = "addressLineNumber")]
        public string AddressLineNumber { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "address")]
    public class Address
    {
        [XmlElement(ElementName = "addressLine")]
        public List<AddressLine> AddressLine { get; set; }
        [XmlElement(ElementName = "street")]
        public string Street { get; set; }
        [XmlElement(ElementName = "cityName")]
        public string CityName { get; set; }
        [XmlElement(ElementName = "country")]
        public string Country { get; set; }
    }

    [XmlRoot(ElementName = "addresses")]
    public class Addresses
    {
        [XmlElement(ElementName = "address")]
        public Address Address { get; set; }
    }

    [XmlRoot(ElementName = "phone")]
    public class Phone
    {
        [XmlAttribute(AttributeName = "tech")]
        public string Tech { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "phones")]
    public class Phones
    {
        [XmlElement(ElementName = "phone")]
        public List<Phone> Phone { get; set; }
    }

    [XmlRoot(ElementName = "emails")]
    public class Emails
    {
        [XmlElement(ElementName = "email")]
        public string Email { get; set; }
    }

    [XmlRoot(ElementName = "urls")]
    public class Urls
    {
        [XmlElement(ElementName = "url")]
        public string Url { get; set; }
    }

    [XmlRoot(ElementName = "geoCode")]
    public class GeoCode
    {
        [XmlElement(ElementName = "latitude")]
        public string Latitude { get; set; }
        [XmlElement(ElementName = "longitude")]
        public string Longitude { get; set; }
        [XmlAttribute(AttributeName = "accuracy")]
        public string Accuracy { get; set; }
    }

    [XmlRoot(ElementName = "geoCodes")]
    public class GeoCodes
    {
        [XmlElement(ElementName = "geoCode")]
        public GeoCode GeoCode { get; set; }
    }

    [XmlRoot(ElementName = "code")]
    public class Code
    {
        [XmlAttribute(AttributeName = "status")]
        public string Status { get; set; }
        [XmlElement(ElementName = "value")]
        public List<Value> Value { get; set; }
    }

    [XmlRoot(ElementName = "provider")]
    public class Provider
    {
        [XmlElement(ElementName = "code")]
        public List<Code> Code { get; set; }
        [XmlAttribute(AttributeName = "providerCode")]
        public string ProviderCode { get; set; }
        [XmlAttribute(AttributeName = "providerType")]
        public string ProviderType { get; set; }
    }

    [XmlRoot(ElementName = "value")]
    public class Value
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "propertyCodes")]
    public class PropertyCodes
    {
        [XmlElement(ElementName = "provider")]
        public List<Provider> Provider { get; set; }
    }

    [XmlRoot(ElementName = "chain")]
    public class Chain
    {
        [XmlAttribute(AttributeName = "chainId")]
        public string ChainId { get; set; }
        [XmlAttribute(AttributeName = "chainName")]
        public string ChainName { get; set; }
    }

    [XmlRoot(ElementName = "chains")]
    public class Chains
    {
        [XmlElement(ElementName = "chain")]
        public Chain Chain { get; set; }
    }

    [XmlRoot(ElementName = "ghgml")]
    public class Ghgml
    {
        [XmlAttribute(AttributeName = "href", Namespace = "http://www.w3.org/1999/xlink")]
        public string Href { get; set; }
    }

    [XmlRoot(ElementName = "property")]
    public class Property
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "alternativeNames")]
        public AlternativeNames AlternativeNames { get; set; }
        [XmlElement(ElementName = "city")]
        public City City { get; set; }
        [XmlElement(ElementName = "destination")]
        public Destination Destination { get; set; }
        [XmlElement(ElementName = "country")]
        public string Country { get; set; }
        [XmlElement(ElementName = "category")]
        public string Category { get; set; }
        [XmlElement(ElementName = "ratings")]
        public Ratings Ratings { get; set; }
        [XmlElement(ElementName = "airports")]
        public Airports Airports { get; set; }
        [XmlElement(ElementName = "addresses")]
        public Addresses Addresses { get; set; }
        [XmlElement(ElementName = "phones")]
        public Phones Phones { get; set; }
        [XmlElement(ElementName = "emails")]
        public Emails Emails { get; set; }
        [XmlElement(ElementName = "urls")]
        public Urls Urls { get; set; }
        [XmlElement(ElementName = "geoCodes")]
        public GeoCodes GeoCodes { get; set; }
        [XmlElement(ElementName = "propertyCodes")]
        public PropertyCodes PropertyCodes { get; set; }
        [XmlElement(ElementName = "chains")]
        public Chains Chains { get; set; }
        [XmlElement(ElementName = "ghgml")]
        public Ghgml Ghgml { get; set; }
        [XmlAttribute(AttributeName = "giataId")]
        public string GiataId { get; set; }
        [XmlAttribute(AttributeName = "lastUpdate")]
        public string LastUpdate { get; set; }
    }

    [XmlRoot(ElementName = "properties")]
    public class Properties : IResponse
    {
        [XmlElement(ElementName = "property")]
        public Property Property { get; set; }
        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }
        [XmlAttribute(AttributeName = "xlink", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xlink { get; set; }
    }

    [XmlRoot(ElementName = "description")]
    public class Description
    {
        [XmlAttribute(AttributeName = "href", Namespace = "http://www.w3.org/1999/xlink")]
        public string Href { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "error")]
    public class Error : IResponse
    {
        [XmlElement(ElementName = "code")]
        public string Code { get; set; }
        [XmlElement(ElementName = "description")]
        public Description Description { get; set; }
        [XmlAttribute(AttributeName = "xlink", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xlink { get; set; }
    }



    #endregion

    /// <summary>
    /// GIATA Database Transfer Model
    /// </summary>
    public class GIATADbTransferModel
    {
        public long? CountryId { get; set; }
        public long AccommodationId { get; set; }
        public long? ChainId { get; set; }
        public string AirportCode { get; set; }
        public string Name { get; set; }
        public string Rating { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string CountryCode { get; set; }
        public string CityName { get; set; }
        public long? CityId { get; set; }
        public string Xml { get; set; }
        public string CountryName { get; set; }
        public string DestinationId { get; set; }
        public string DestinationCode { get; set; }
        public string Email { get; set; }
        public string Category { get; set; }
        public string Url { get; set; }
        public List<AlternativeNameViewModel> AlternativeNames { get; set; }
        public List<Airport> Airports { get; set; }
        public ConcurrentBag<AccommodationSupplier> Suppliers { get; set; }
    }
    public class AlternativeNameViewModel
    {
        public string Name { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string Type { get; set; }
    }
    public class AccommodationSupplier
    {
        public string Code { get; set; }
        public bool Active { get; set; }
        public string ProviderType { get; set; }
        public string ProviderCode { get; set; }
        public string ProviderValue { get; set; }
    }

    public class MapResult
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public bool ServiceSuccess { get; set; }
        public bool MapToDbSuccess { get; set; }
    }

    public class GIATAFile
    {
        public string Name { get; set; }
        public string Extention { get; set; }
        public byte[] Contents { get; set; }
    }
}
