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
    
    public partial class WeatherLocationSearch
    {
        public int Id { get; set; }
        public long LocationID { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public Nullable<bool> IsError { get; set; }
        public string ErrorMessage { get; set; }
        public Nullable<int> ErrorNumber { get; set; }
        public Nullable<bool> IsCheck { get; set; }
    }
}
