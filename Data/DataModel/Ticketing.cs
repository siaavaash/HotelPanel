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
    
    public partial class Ticketing
    {
        public int TicketingId { get; set; }
        public int IssuanceDataId { get; set; }
        public string eTicketNumber { get; set; }
        public string Ind { get; set; }
        public string UsedCount { get; set; }
    
        public virtual IssuanceData IssuanceData { get; set; }
    }
}
