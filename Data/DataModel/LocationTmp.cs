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
    
    public partial class LocationTmp
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
    }
}
