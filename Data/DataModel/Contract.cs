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
    
    public partial class Contract
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contract()
        {
            this.AccomodatonContracts = new HashSet<AccomodatonContract>();
            this.Contacts1 = new HashSet<Contacts1>();
            this.ContractRefunds = new HashSet<ContractRefund>();
            this.FlightContracts = new HashSet<FlightContract>();
            this.InsuranceContracts = new HashSet<InsuranceContract>();
            this.Payments = new HashSet<Payment>();
            this.PNRs = new HashSet<PNR>();
            this.RailwayContracts = new HashSet<RailwayContract>();
            this.RequestObjects = new HashSet<RequestObject>();
            this.RequestTours = new HashSet<RequestTour>();
            this.ReserveRefounds = new HashSet<ReserveRefound>();
            this.ServiceContracts = new HashSet<ServiceContract>();
            this.TransferContracts = new HashSet<TransferContract>();
            this.Wallets = new HashSet<Wallet>();
        }
    
        public long ContractID { get; set; }
        public Nullable<long> UserID { get; set; }
        public string Description { get; set; }
        public Nullable<long> Id { get; set; }
        public Nullable<long> OrderNo { get; set; }
        public Nullable<long> PatronUserID { get; set; }
        public Nullable<byte> Status { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<System.DateTime> CreationDatetime { get; set; }
        public Nullable<long> ModificationUserID { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccomodatonContract> AccomodatonContracts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contacts1> Contacts1 { get; set; }
        public virtual User1 User1 { get; set; }
        public virtual User1 User11 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContractRefund> ContractRefunds { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FlightContract> FlightContracts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InsuranceContract> InsuranceContracts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PNR> PNRs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RailwayContract> RailwayContracts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequestObject> RequestObjects { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequestTour> RequestTours { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReserveRefound> ReserveRefounds { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceContract> ServiceContracts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransferContract> TransferContracts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}
