using Data.DataModel;
using Service.ServiceModel.BookingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Logic.BusinessObjects
{
    public class BookingBusiness
    {
        public bool InsertHotelInfo(long hotelID, HotelData data)
        {
            try
            {
                using (var context = new BookingStaticDataEntities())
                {
                    var hotel = context.Hotels.FirstOrDefault(x => x.HotelId == hotelID);
                    hotel.DescriptionSite = data.Description;
                    context.GoodToNows.Add(new GoodToNow
                    {
                        CheckIn = data.GoodToKnow.CheckIn,
                        CheckOut = data.GoodToKnow.CheckOut,
                        HotelId = hotelID,
                        Pets = data.GoodToKnow.Pets,
                    });
                    context.ImgUrls.AddRange(data.HotelImageUrls.Select(x => new ImgUrl
                    {
                        HotelId = hotelID,
                        Path = x,
                        LastUpdate = DateTime.Now,
                    }).ToList());
                    context.Locations.AddRange(data.Locations.Select(x => new Data.DataModel.Location
                    {
                        Name = x.Name,
                        LocationID = x.LocationID,
                        LastUpdate = DateTime.Now
                    }).ToList());
                    context.HotelLatLongs.Add(new HotelLatLong
                    {
                        HotelId = hotelID,
                        Lat = data.Latitude,
                        Long = data.Longitude,
                    });
                    context.Facilities.AddRange(data.Facilities.Select(x => new Data.DataModel.Facility
                    {
                        Category = x.Category,
                        HotelId = hotelID,
                        Title = x.Title,
                        LastUpdate = DateTime.Now
                    }).ToList());
                    return context.SaveChanges() > 0 ? true : false;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool InsertRoomInfo(long hotelID, List<RoomData> data)
        {
            try
            {
                using (var context = new BookingStaticDataEntities())
                {
                    Parallel.ForEach(data, roomData =>
                    {
                        roomData.RoomImages.ForEach(image =>
                        {
                            context.RoomInfoes.Add(new RoomInfo
                            {
                                BookingId = hotelID.ToString(),
                                Radif = image.ID,
                                RoomSize = roomData.RoomSize,
                                RoomTypeIcon = roomData.Sleeps,
                                RoomTypeName = roomData.RoomType,
                                RoomTypeInfo = roomData.RoomTypeInfo,
                                RoomTypeIconInfo = roomData.SleepsInfo,
                                RoomImgUrl = image.Url,
                                RoomFacilities = string.Join("-", roomData.Facilities),
                                RoomDescription = roomData.Description,
                                RoomTypeId = Convert.ToInt64(Regex.Replace(roomData.RoomTypeID, "[A-Za-z ]", "")),
                            });
                        });
                    });
                    return context.SaveChanges() > 0 ? true : false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
