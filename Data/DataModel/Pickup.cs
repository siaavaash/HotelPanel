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
    
    public partial class Pickup
    {
        public long PicKupId { get; set; }
        public Nullable<long> LocationID { get; set; }
        public byte Type { get; set; }
        public long BatchID { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string Continent { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Address { get; set; }
        public string CityName { get; set; }
        public string StateProv { get; set; }
        public string StateCode { get; set; }
        public string PostalCode { get; set; }
        public string AirportCode { get; set; }
        public Nullable<bool> Airport { get; set; }
        public Nullable<bool> RailwayStation { get; set; }
        public bool Active { get; set; }
        public string IANATimeZone { get; set; }
    
        public virtual Location Location { get; set; }
    }
}
