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
    
    public partial class PNR_RPH
    {
        public long PNR_RPHID { get; set; }
        public Nullable<long> PNRID { get; set; }
        public string RPH { get; set; }
        public string RPHKey { get; set; }
        public string PNR { get; set; }
        public Nullable<long> SupplierID { get; set; }
        public string RequestId { get; set; }
    
        public virtual PNR PNR1 { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}