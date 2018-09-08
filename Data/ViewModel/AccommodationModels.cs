using Data.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel
{
    public class AccommodationModels
    {
        public class ListNameAccommodation
        {
            public long AccommodationID { get; set; }
            public string Name { get; set; }
            public string CityName { get; set; }
            public string Country { get; set; }
            public Nullable<System.DateTime> lastUpdate { get; set; }
        }
        public class FilterImages
        {
            public long AccommodationID { get; set; }
            public List<long> ImageID { get; set; }
        }
        public class FilterImagesView
        {
            public long AccommodationID { get; set; }
            public List<AccomodationImage> AccomodationImages { get; set; }
        }
        public class RoomImagesViewModel
        {
            public long AccommodationID { get; set; }
            public List<AccomodationRoomImage> RoomImages { get; set; }
        }

        public class SearchAccommodation
        {
            public long? AccommodationID { get; set; }
            public string Name { get; set; }
            public string CityName { get; set; }
            public string Country { get; set; }
            public long? From { get; set; }
            public long? To { get; set; }
            public bool Verified { get; set; }
        }
        public class AccommodationFacility
        {
            public long AccommodationID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public List<Facility> Facilities { get; set; }
        }
        public class EditName
        {
            public long AccommodationID { get; set; }
            public string Name { get; set; }
        }
        public class EditDescription
        {
            public long AccommodationID { get; set; }
            public string Description { get; set; }
        }
        public class ListFacilities
        {
            public long AccommodationID { get; set; }
            public string AccommodationName { get; set; }
            public List<Facility> Facilities { get; set; }
        }
        public class AddFacilities
        {
            public long AccommodationID { get; set; }
            public List<long> FacilityID { get; set; }
        }
        public class EditFacilities
        {
            public long AccommodationID { get; set; }
            public long FacilityID { get; set; }
            public string Name { get; set; }
        }
        public class ListLanguageDescription
        {
            public long? AccommodationID { get; set; }
            public List<Data.DataModel.AccommodationDescription> AccommodationDescriptions { get; set; }
        }
        public class AddDescriptionViewModel
        {
            public long? AccommodationID { get; set; }
            public string Description { get; set; }
        }
        public class EditDescriptionViewModel
        {
            public long AccommodationDescriptionID { get; set; }
            public long? AccommodationID { get; set; }
            public string Description { get; set; }
        }
        public class AddDescription
        {
            public long? AccommodationID { get; set; }
            public long?LanguageID { get; set; }
            public string Description { get; set; }
            public System.DateTime? CreationTime { get; set; }
            public System.DateTime? ModificationTime { get; set; }
            public long? UserID { get; set; }
            public long? ModifyUserID { get; set; }
        }
        public class ChangeDescription
        {
            public long AccommodationDescriptionID { get; set; }
            public long? AccommodationID { get; set; }
            public long? LanguageID { get; set; }
            public string Description { get; set; }
            public System.DateTime? ModificationTime { get; set; }
            public long? ModifyUserID { get; set; }
        }
    }
}
