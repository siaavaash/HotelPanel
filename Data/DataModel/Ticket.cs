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
    
    public partial class Ticket
    {
        public long TicketID { get; set; }
        public string TicketNo { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PassengerType { get; set; }
        public Nullable<int> Age { get; set; }
        public string FlightNumber { get; set; }
        public string Cabin { get; set; }
        public Nullable<int> BaggageAllowance { get; set; }
        public Nullable<float> BaseFareAmount { get; set; }
        public string BaseFareCurrency { get; set; }
        public Nullable<float> FareTaxAmount { get; set; }
        public string FareTaxCurrency { get; set; }
        public Nullable<float> EquivFareAmount { get; set; }
        public string EquivFareCurrency { get; set; }
        public Nullable<float> TotalFareAmount { get; set; }
        public string TotalFareCurrency { get; set; }
        public string FareCalculation { get; set; }
        public string DepartDate { get; set; }
        public string Dest { get; set; }
        public string Origin { get; set; }
        public string NotValidAfter { get; set; }
        public string FareBasis { get; set; }
        public string AccountingCode { get; set; }
        public string IssueDate { get; set; }
        public string IssuingAgent { get; set; }
        public Nullable<float> CashCheckAMount { get; set; }
        public string PaymentForm { get; set; }
        public string ItineraryCode { get; set; }
        public string PrintStation { get; set; }
        public string TicketInternalNo { get; set; }
        public string UnsuccessMessage { get; set; }
        public Nullable<int> Status { get; set; }
        public string Carrier { get; set; }
        public string PayStatus { get; set; }
        public string CompleteBy { get; set; }
        public string CompleteDate { get; set; }
        public string Attach { get; set; }
        public string GDS { get; set; }
        public string PCC { get; set; }
        public string Commission { get; set; }
        public string ExchangeRate { get; set; }
        public string Markup { get; set; }
        public long UserID { get; set; }
        public Nullable<long> PNRID { get; set; }
        public string AirLineRef { get; set; }
        public string PNRCode { get; set; }
    
        public virtual PNR PNR { get; set; }
        public virtual User1 User1 { get; set; }
    }
}