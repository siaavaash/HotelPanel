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
    
    public partial class CompanyName
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CompanyName()
        {
            this.TransferContracts = new HashSet<TransferContract>();
        }
    
        public long CompanyNameid { get; set; }
        public string VAT { get; set; }
        public string CompanyNameField { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransferContract> TransferContracts { get; set; }
    }
}