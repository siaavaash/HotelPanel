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
    
    public partial class Banner
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public long WebSiteID { get; set; }
        public Nullable<long> PositionID { get; set; }
    
        public virtual WebSite WebSite { get; set; }
    }
}