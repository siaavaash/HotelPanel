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
    
    public partial class Location961114
    {
        public long LocationID { get; set; }
        public long ParentLocationID { get; set; }
        public long LocationTypeID { get; set; }
        public string SubClass { get; set; }
        public string Name { get; set; }
        public string NameLong { get; set; }
        public Nullable<System.DateTime> lastUpdate { get; set; }
        public string CountryCode { get; set; }
        public string CityName { get; set; }
        public Nullable<long> CityId { get; set; }
        public string AirportCode { get; set; }
        public string NamePath { get; set; }
        public Nullable<long> destinationId { get; set; }
        public string DestinationCode { get; set; }
        public Nullable<byte> Rank { get; set; }
        public Nullable<long> ParentLocation2ID { get; set; }
        public Nullable<int> TypeID { get; set; }
        public Nullable<long> AccommodationlID { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public System.Data.Entity.Spatial.DbGeography GeoLocation { get; set; }
        public string AirportCodeNear { get; set; }
        public string HotelsProRegion { get; set; }
        public string FullName { get; set; }
    }
}
