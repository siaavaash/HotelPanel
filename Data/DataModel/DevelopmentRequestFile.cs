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
    
    public partial class DevelopmentRequestFile
    {
        public long DevelopmentRequestFileID { get; set; }
        public Nullable<long> DevelopmentRequestID { get; set; }
        public string IMG_Name { get; set; }
        public string IMG_Type { get; set; }
    
        public virtual DevelopmentRequest DevelopmentRequest { get; set; }
    }
}
