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
    
    public partial class IP
    {
        public long IPID { get; set; }
        public long LocationID { get; set; }
        public long StartIP { get; set; }
        public long EndIP { get; set; }
    
        public virtual Location Location { get; set; }
    }
}
