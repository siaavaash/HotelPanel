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
    
    public partial class Token
    {
        public long TokenID { get; set; }
        public long UserId { get; set; }
        public string AccessToken { get; set; }
        public System.DateTime Issued { get; set; }
        public System.DateTime Expires { get; set; }
    }
}
