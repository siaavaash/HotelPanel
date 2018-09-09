using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel.VerifyPanelViewModels
{
    public class HotelInfoViewModel
    {
        public long AccommodationId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Rating { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string BookingUrl { get; set; }
        public long? CityId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsVerified { get; set; }
        public string VerifiedDate { get; set; }
        public List<Image> RoomImages { get; set; }
        public List<Image> AccommodationImages { get; set; }
        public List<Facility> AccommodationFacilities { get; set; }
    }
    public class Image
    {
        public long Id { get; set; }
        public long AccommodationId { get; set; }
        public string Url { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsVerified { get; set; }
        public bool? IsReported { get; set; }
        public int? NumberOfReports { get; set; }
        public DateTime? VerifiedDate { get; set; }
    }
    public class Facility
    {
        public long Id { get; set; }
        public long AccommodationId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
    }
    public class AutocompleteViewModel
    {
        public string Country { get; set; }
        public string NameLong { get; set; }
        public string Text { get; set; }
        public long Id { get; set; }
    }
}
