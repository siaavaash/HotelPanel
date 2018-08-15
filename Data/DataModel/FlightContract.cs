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
    
    public partial class FlightContract
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FlightContract()
        {
            this.FlightTickets = new HashSet<FlightTicket>();
        }
    
        public long FlightContractID { get; set; }
        public long ContractID { get; set; }
        public string RPHKey_Old { get; set; }
        public string RPH { get; set; }
        public string PNR { get; set; }
        public string FlightReservName { get; set; }
        public Nullable<decimal> TotalFare { get; set; }
        public string TotalFareCurrencyCode { get; set; }
        public Nullable<decimal> TotalCommission { get; set; }
        public string TotalCommissionCurrencyCode { get; set; }
        public Nullable<decimal> CounterCommission { get; set; }
        public string CounterCommissionCurrencyCode { get; set; }
        public Nullable<long> TicketStatusID { get; set; }
        public string RPHKey { get; set; }
        public string BillTicket { get; set; }
    
        public virtual Contract Contract { get; set; }
        public virtual TicketStatu TicketStatu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FlightTicket> FlightTickets { get; set; }
    }
}