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
    
    public partial class FlightTicketNotArrival
    {
        public long FlightTicketNotArrivalID { get; set; }
        public long FlightTicketID { get; set; }
        public string NotArrivalCityName { get; set; }
        public string NotArrivalCountryName { get; set; }
        public string NotArrivalAirportCode { get; set; }
        public string NotArrivalDate { get; set; }
        public string NotArrivalTime { get; set; }
        public string NotArrivalAirportName { get; set; }
        public string NotArrivalFlightNumber { get; set; }
        public string CabinClasses { get; set; }
        public string FlightRoute { get; set; }
        public Nullable<bool> BagAllowed { get; set; }
        public Nullable<byte> BagAllowance { get; set; }
        public string BagAllowanceUnit { get; set; }
        public Nullable<bool> CarryOnAllowed { get; set; }
        public Nullable<byte> CarryOnAllowance { get; set; }
        public string CarryOnAllowanceUnit { get; set; }
        public string MoreInfo { get; set; }
    
        public virtual FlightTicket FlightTicket { get; set; }
    }
}
