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
    
    public partial class Payment1
    {
        public long PaymentID { get; set; }
        public long CompanyID { get; set; }
        public long CurrencyID { get; set; }
        public long ContractID { get; set; }
        public string TransactionID { get; set; }
        public byte Status { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime PaymentDateTime { get; set; }
        public Nullable<int> PaymentType { get; set; }
        public string Comment { get; set; }
        public string Image { get; set; }
        public Nullable<long> ReversedFromID { get; set; }
        public Nullable<System.DateTime> CreationDatetime { get; set; }
    }
}
