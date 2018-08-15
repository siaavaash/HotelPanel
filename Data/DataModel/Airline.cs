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
    
    public partial class Airline
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Airline()
        {
            this.AirlineSupplierConfigs = new HashSet<AirlineSupplierConfig>();
            this.AirlineSupplierWhitelists = new HashSet<AirlineSupplierWhitelist>();
            this.AirportSupplierConfig1 = new HashSet<AirportSupplierConfig1>();
        }
    
        public long AirlineID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Image { get; set; }
        public string NorTel { get; set; }
        public string NorEmail { get; set; }
        public string Logo { get; set; }
        public Nullable<long> CountryID { get; set; }
        public string PersianName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AirlineSupplierConfig> AirlineSupplierConfigs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AirlineSupplierWhitelist> AirlineSupplierWhitelists { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AirportSupplierConfig1> AirportSupplierConfig1 { get; set; }
    }
}
