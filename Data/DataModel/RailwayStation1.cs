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
    
    public partial class RailwayStation1
    {
        public long RailwayStationID { get; set; }
        public long LocationID { get; set; }
        public byte TYPE { get; set; }
        public string Code { get; set; }
        public string NAME { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool ACTIVE { get; set; }
    
        public virtual Location Location { get; set; }
    }
}
