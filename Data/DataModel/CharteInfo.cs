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
    
    public partial class CharteInfo
    {
        public long CharteInfoID { get; set; }
        public Nullable<long> DistinationID { get; set; }
        public Nullable<long> OrigiinID { get; set; }
        public Nullable<System.DateTime> DepartureDate { get; set; }
        public Nullable<System.DateTime> DepartureTime { get; set; }
        public Nullable<long> AirLineID { get; set; }
        public Nullable<int> AirLineTypeID { get; set; }
        public string FlightNumber { get; set; }
        public Nullable<int> Capacity { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreationDateTime { get; set; }
        public Nullable<System.DateTime> ModificationDateTime { get; set; }
        public Nullable<System.DateTime> LastChangeTime { get; set; }
        public Nullable<long> CreatorUserID { get; set; }
        public Nullable<long> ModificationUserID { get; set; }
    }
}