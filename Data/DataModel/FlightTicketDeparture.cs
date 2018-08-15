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
    
    public partial class FlightTicketDeparture
    {
        public long FlightTicketDepartureID { get; set; }
        public long FlightTicketID { get; set; }
        public string DepartureCityName { get; set; }
        public string DepartureCountryName { get; set; }
        public string DepartureAirportCode { get; set; }
        public string DepartureDate { get; set; }
        public string DepartureTime { get; set; }
        public string DepartureAirportName { get; set; }
        public string DepartureFlightNumber { get; set; }
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
