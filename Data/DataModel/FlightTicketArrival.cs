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
    
    public partial class FlightTicketArrival
    {
        public long FlightTicketArrivalID { get; set; }
        public long FlightTicketID { get; set; }
        public string ArrivalCityName { get; set; }
        public string ArrivalCountryName { get; set; }
        public string ArrivalAirportCode { get; set; }
        public string ArrivalDate { get; set; }
        public string ArrivalTime { get; set; }
        public string ArrivalAirportName { get; set; }
        public string Rturn { get; set; }
        public string Change { get; set; }
        public string ArrivalFlightNumber { get; set; }
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
