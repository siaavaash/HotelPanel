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
    
    public partial class AccomodationSupplier960616
    {
        public long AccomodationSupplierID { get; set; }
        public long AccommodationlID { get; set; }
        public long SupplierID { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
        public string ProviderType { get; set; }
        public string ProviderCode { get; set; }
        public string ProviderValue { get; set; }
        public Nullable<System.DateTime> lastUpdate { get; set; }
        public string CountryCode { get; set; }
        public string CityName { get; set; }
        public Nullable<long> CityId { get; set; }
        public string CountryName { get; set; }
    }
}
