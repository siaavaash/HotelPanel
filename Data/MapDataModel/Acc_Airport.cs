//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data.MapDataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Acc_Airport
    {
        public long AirPortID { get; set; }
        public Nullable<long> AccommodationlID { get; set; }
        public string AirPort_Code { get; set; }
        public Nullable<long> destinationId { get; set; }
        public string DestinationCode { get; set; }
        public string category { get; set; }
        public string rating { get; set; }
        public Nullable<System.DateTime> lastUpdate { get; set; }
        public string CountryCode { get; set; }
        public string CityName { get; set; }
        public Nullable<long> CityId { get; set; }
    }
}