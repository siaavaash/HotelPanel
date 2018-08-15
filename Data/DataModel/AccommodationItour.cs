//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class AccommodationItour
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AccommodationItour()
        {
            this.AccommodationItoursSuppliers = new HashSet<AccommodationItoursSupplier>();
        }
    
        public long AccommodationlID { get; set; }
        public Nullable<long> ChainID { get; set; }
        public string AirportCode { get; set; }
        public Nullable<byte> Type { get; set; }
        public string Name { get; set; }
        public string Rating { get; set; }
        public string Address { get; set; }
        public string PostCodeZip { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Description { get; set; }
        public string Near { get; set; }
        public Nullable<System.DateTime> lastUpdate { get; set; }
        public string CountryCode { get; set; }
        public string CityName { get; set; }
        public Nullable<long> CityId { get; set; }
        public string Resort { get; set; }
        public string Region { get; set; }
        public string DestinationId { get; set; }
        public string Destination { get; set; }
        public string Country { get; set; }
        public string HotelArea { get; set; }
        public string HotelImages { get; set; }
        public string HotelLocation { get; set; }
        public string HotelInfo { get; set; }
        public string PAmenities { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> UserID { get; set; }
        public Nullable<System.DateTime> DateVerified { get; set; }
        public Nullable<bool> IsVerified { get; set; }
        public Nullable<bool> IsError { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccommodationItoursSupplier> AccommodationItoursSuppliers { get; set; }
    }
}
