using BookingDB;
using Service.ServiceModel.PublicModels;
using System.Collections.Generic;

namespace Service.ServiceModel.BookingModels
{
    public class UrlModel
    {
        public long ID { get; set; }
        public string Url { get; set; }
    }
    public class BookingResponseModel<T>
    {
        public bool Success { get; set; }
        public List<Error> Errors { get; set; }
        public T Data { get; set; }
    }
    public class HotelData
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public List<string> HotelImageUrls { get; set; }
        public List<Facility> Facilities { get; set; }
        public GoodToNow GoodToKnow { get; set; }
        public List<Location> Locations { get; set; }
    }
    public class RoomData
    {
        public string Sleeps { get; set; }
        public string RoomType { get; set; }
        public List<RoomImage> RoomImages { get; set; }
        public string RoomSize { get; set; }
        public List<string> Facilities { get; set; }
        public string SleepsInfo { get; set; }
        public string RoomTypeInfo { get; set; }
        public string Description { get; set; }
        public string RoomTypeID { get; set; }
    }
    public class RoomImage
    {
        public int ID { get; set; }
        public string Url { get; set; }
    }

    public class BookingViewModel
    {
        public long HotelID { get; set; }
        public string Message { get; set; }
        public bool GetRoomInfo { get; set; }
        public bool GetHotelInfo { get; set; }
        public bool InsertHotelToDB { get; set; }
        public bool InsertRoomsToDB { get; set; }
    }
}
