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
    
    public partial class PersonNameList
    {
        public long PersonNameid { get; set; }
        public long Personid { get; set; }
        public string NamePrefix { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
    
        public virtual Person Person { get; set; }
    }
}
