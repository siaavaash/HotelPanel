using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Service.ServiceModel.GIATAModels
{

    #region GIATA Response Model

    public interface IResponse { }



    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = true)]
    public partial class properties : IResponse
    {

        private propertiesProperty propertyField;

        /// <remarks/>
        public propertiesProperty property
        {
            get
            {
                return this.propertyField;
            }
            set
            {
                this.propertyField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true)]
    public partial class propertiesProperty
    {

        private string nameField;

        private propertiesPropertyAlternativeName[] alternativeNamesField;

        private propertiesPropertyCity cityField;

        private propertiesPropertyDestination destinationField;

        private string countryField;

        private byte? categoryField;

        private propertiesPropertyRatings ratingsField;

        private propertiesPropertyAirport[] airportsField;

        private propertiesPropertyAddresses addressesField;

        private propertiesPropertyPhone[] phonesField;

        private propertiesPropertyEmails emailsField;

        private propertiesPropertyUrls urlsField;

        private propertiesPropertyGeoCodes geoCodesField;

        private propertiesPropertyProvider[] propertyCodesField;

        private propertiesPropertyChains chainsField;

        private propertiesPropertyGhgml ghgmlField;

        private int giataIdField;

        private System.DateTime? lastUpdateField;

        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [XmlArrayItem("alternativeName", IsNullable = true)]
        public propertiesPropertyAlternativeName[] alternativeNames
        {
            get
            {
                return this.alternativeNamesField;
            }
            set
            {
                this.alternativeNamesField = value;
            }
        }

        /// <remarks/>
        public propertiesPropertyCity city
        {
            get
            {
                return this.cityField;
            }
            set
            {
                this.cityField = value;
            }
        }

        /// <remarks/>
        public propertiesPropertyDestination destination
        {
            get
            {
                return this.destinationField;
            }
            set
            {
                this.destinationField = value;
            }
        }

        /// <remarks/>
        public string country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                this.countryField = value;
            }
        }

        /// <remarks/>
        public byte? category
        {
            get
            {
                return this.categoryField;
            }
            set
            {
                this.categoryField = value;
            }
        }

        /// <remarks/>
        public propertiesPropertyRatings ratings
        {
            get
            {
                return this.ratingsField;
            }
            set
            {
                this.ratingsField = value;
            }
        }

        /// <remarks/>
        [XmlArrayItem("airport", IsNullable = true)]
        public propertiesPropertyAirport[] airports
        {
            get
            {
                return this.airportsField;
            }
            set
            {
                this.airportsField = value;
            }
        }

        /// <remarks/>
        public propertiesPropertyAddresses addresses
        {
            get
            {
                return this.addressesField;
            }
            set
            {
                this.addressesField = value;
            }
        }

        /// <remarks/>
        [XmlArrayItem("phone", IsNullable = true)]
        public propertiesPropertyPhone[] phones
        {
            get
            {
                return this.phonesField;
            }
            set
            {
                this.phonesField = value;
            }
        }

        /// <remarks/>
        public propertiesPropertyEmails emails
        {
            get
            {
                return this.emailsField;
            }
            set
            {
                this.emailsField = value;
            }
        }

        /// <remarks/>
        public propertiesPropertyUrls urls
        {
            get
            {
                return this.urlsField;
            }
            set
            {
                this.urlsField = value;
            }
        }

        /// <remarks/>
        public propertiesPropertyGeoCodes geoCodes
        {
            get
            {
                return this.geoCodesField;
            }
            set
            {
                this.geoCodesField = value;
            }
        }

        /// <remarks/>
        [XmlArrayItem("provider", IsNullable = true)]
        public propertiesPropertyProvider[] propertyCodes
        {
            get
            {
                return this.propertyCodesField;
            }
            set
            {
                this.propertyCodesField = value;
            }
        }

        /// <remarks/>
        public propertiesPropertyChains chains
        {
            get
            {
                return this.chainsField;
            }
            set
            {
                this.chainsField = value;
            }
        }

        /// <remarks/>
        public propertiesPropertyGhgml ghgml
        {
            get
            {
                return this.ghgmlField;
            }
            set
            {
                this.ghgmlField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public int giataId
        {
            get
            {
                return this.giataIdField;
            }
            set
            {
                this.giataIdField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public System.DateTime? lastUpdate
        {
            get
            {
                return this.lastUpdateField;
            }
            set
            {
                this.lastUpdateField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true)]
    public partial class propertiesPropertyAlternativeName
    {

        private string alternativeNameTypeField;

        private DateTime? effectiveDateField;

        private bool? effectiveDateFieldSpecified;

        private string valueField;

        /// <remarks/>
        [XmlAttribute()]
        public string alternativeNameType
        {
            get
            {
                return this.alternativeNameTypeField;
            }
            set
            {
                this.alternativeNameTypeField = value;
            }
        }

        /// <remarks/>
        //[XmlAttribute(DataType = "date")]
        public System.DateTime? effectiveDate
        {
            get
            {
                return this.effectiveDateField;
            }
            set
            {
                this.effectiveDateField = value;
            }
        }

        /// <remarks/>
        public bool? effectiveDateSpecified
        {
            get
            {
                return this.effectiveDateFieldSpecified;
            }
            set
            {
                this.effectiveDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true)]
    public partial class propertiesPropertyCity
    {

        private ushort? cityIdField;

        private string valueField;

        /// <remarks/>
        [XmlAttribute()]
        public ushort? cityId
        {
            get
            {
                return this.cityIdField;
            }
            set
            {
                this.cityIdField = value;
            }
        }

        /// <remarks/>
        [XmlText()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true)]
    public partial class propertiesPropertyDestination
    {

        private ushort? destinationIdField;

        private string valueField;

        /// <remarks/>
        [XmlAttribute()]
        public ushort? destinationId
        {
            get
            {
                return this.destinationIdField;
            }
            set
            {
                this.destinationIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true)]
    public partial class propertiesPropertyRatings
    {

        private propertiesPropertyRatingsRating ratingField;

        /// <remarks/>
        public propertiesPropertyRatingsRating rating
        {
            get
            {
                return this.ratingField;
            }
            set
            {
                this.ratingField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true)]
    public partial class propertiesPropertyRatingsRating
    {

        private byte? valueField;

        private bool? isDefaultField;

        /// <remarks/>
        [XmlAttribute()]
        public byte? value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public bool? isDefault
        {
            get
            {
                return this.isDefaultField;
            }
            set
            {
                this.isDefaultField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class propertiesPropertyAirport
    {

        private string iataField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string iata
        {
            get
            {
                return this.iataField;
            }
            set
            {
                this.iataField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class propertiesPropertyAddresses
    {

        private propertiesPropertyAddressesAddress addressField;

        /// <remarks/>
        public propertiesPropertyAddressesAddress address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class propertiesPropertyAddressesAddress
    {

        private propertiesPropertyAddressesAddressAddressLine[] addressLineField;

        private string streetField;

        private string cityNameField;

        private string countryField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("addressLine")]
        public propertiesPropertyAddressesAddressAddressLine[] addressLine
        {
            get
            {
                return this.addressLineField;
            }
            set
            {
                this.addressLineField = value;
            }
        }

        /// <remarks/>
        public string street
        {
            get
            {
                return this.streetField;
            }
            set
            {
                this.streetField = value;
            }
        }

        /// <remarks/>
        public string cityName
        {
            get
            {
                return this.cityNameField;
            }
            set
            {
                this.cityNameField = value;
            }
        }

        /// <remarks/>
        public string country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                this.countryField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class propertiesPropertyAddressesAddressAddressLine
    {

        private byte? addressLineNumberField;

        private string valueField;

        /// <remarks/>
        [XmlAttribute()]
        public byte? addressLineNumber
        {
            get
            {
                return this.addressLineNumberField;
            }
            set
            {
                this.addressLineNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class propertiesPropertyPhone
    {

        private string techField;

        private long? valueField;

        /// <remarks/>
        [XmlAttribute()]
        public string tech
        {
            get
            {
                return this.techField;
            }
            set
            {
                this.techField = value;
            }
        }

        /// <remarks/>
        [XmlText()]
        public long? Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class propertiesPropertyEmails
    {

        private string emailField;

        /// <remarks/>
        public string email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true)]
    public partial class propertiesPropertyUrls
    {

        private string urlField;

        /// <remarks/>
        public string url
        {
            get
            {
                return this.urlField;
            }
            set
            {
                this.urlField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true)]
    public partial class propertiesPropertyGeoCodes
    {

        private propertiesPropertyGeoCodesGeoCode geoCodeField;

        /// <remarks/>
        public propertiesPropertyGeoCodesGeoCode geoCode
        {
            get
            {
                return this.geoCodeField;
            }
            set
            {
                this.geoCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class propertiesPropertyGeoCodesGeoCode
    {

        private decimal? latitudeField;

        private decimal? longitudeField;

        private string accuracyField;

        /// <remarks/>
        public decimal? latitude
        {
            get
            {
                return this.latitudeField;
            }
            set
            {
                this.latitudeField = value;
            }
        }

        /// <remarks/>
        public decimal? longitude
        {
            get
            {
                return this.longitudeField;
            }
            set
            {
                this.longitudeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string accuracy
        {
            get
            {
                return this.accuracyField;
            }
            set
            {
                this.accuracyField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class propertiesPropertyProvider
    {

        private propertiesPropertyProviderCode[] codeField;

        private string providerCodeField;

        private string providerTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("code")]
        public propertiesPropertyProviderCode[] code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string providerCode
        {
            get
            {
                return this.providerCodeField;
            }
            set
            {
                this.providerCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string providerType
        {
            get
            {
                return this.providerTypeField;
            }
            set
            {
                this.providerTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class propertiesPropertyProviderCode
    {

        private propertiesPropertyProviderCodeValue[] valueField;

        private string statusField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("value")]
        public propertiesPropertyProviderCodeValue[] value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class propertiesPropertyProviderCodeValue
    {

        private string nameField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class propertiesPropertyChains
    {

        private propertiesPropertyChainsChain chainField;

        /// <remarks/>
        public propertiesPropertyChainsChain chain
        {
            get
            {
                return this.chainField;
            }
            set
            {
                this.chainField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class propertiesPropertyChainsChain
    {

        private ushort? chainIdField;

        private string chainNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort? chainId
        {
            get
            {
                return this.chainIdField;
            }
            set
            {
                this.chainIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string chainName
        {
            get
            {
                return this.chainNameField;
            }
            set
            {
                this.chainNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class propertiesPropertyGhgml
    {

        private string hrefField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/1999/xlink")]
        public string href
        {
            get
            {
                return this.hrefField;
            }
            set
            {
                this.hrefField = value;
            }
        }
    }


    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = true)]
    public partial class error : IResponse
    {

        private ushort? codeField;

        private errorDescription descriptionField;

        /// <remarks/>
        public ushort? code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        public errorDescription description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class errorDescription
    {

        private string hrefField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/1999/xlink")]
        public string href
        {
            get
            {
                return this.hrefField;
            }
            set
            {
                this.hrefField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
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
        public List<propertiesPropertyAirport> Airports { get; set; }
        public List<AccommodationSupplier> Suppliers { get; set; }
    }
    public class AlternativeName
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
}
