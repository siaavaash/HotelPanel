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
    
    public partial class DevelopmentRequestDetail
    {
        public long DevelopmentRequestDetailID { get; set; }
        public Nullable<long> DevelopmnetRequestID { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }
        public Nullable<long> FromID { get; set; }
    
        public virtual DevelopmentRequest DevelopmentRequest { get; set; }
        public virtual User1 User1 { get; set; }
    }
}
