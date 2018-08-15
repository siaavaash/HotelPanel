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
    
    public partial class Wallet
    {
        public long WalletID { get; set; }
        public long CompanyID { get; set; }
        public long PaymentID { get; set; }
        public long ContractID { get; set; }
        public long CurrencyID { get; set; }
        public decimal Amount { get; set; }
        public long ExchangeCurrencyID { get; set; }
        public decimal ExchangeAmount { get; set; }
        public System.DateTime WalletDateTime { get; set; }
        public Nullable<int> WalletType { get; set; }
        public byte Status { get; set; }
        public string Comment { get; set; }
        public Nullable<long> CreationUserID { get; set; }
        public Nullable<System.DateTime> CreationDatetime { get; set; }
        public Nullable<long> ModificationUserID { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
        public string AgnecyName { get; set; }
        public string FullName { get; set; }
        public string NationalCode { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public Nullable<byte> DecimalPlaces { get; set; }
        public string Code { get; set; }
        public string Telephone { get; set; }
        public string CompanyName { get; set; }
        public string ResponseWallet { get; set; }
    
        public virtual Company1 Company1 { get; set; }
        public virtual Contract Contract { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Currency Currency1 { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual User1 User1 { get; set; }
        public virtual User1 User11 { get; set; }
    }
}