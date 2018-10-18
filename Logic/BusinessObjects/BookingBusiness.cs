using BookingDB;
using Service.ServiceModel.BookingModels;
using Service.Suppliers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Logic.BusinessObjects
{
    public class BookingBusiness
    {
        private static readonly Lazy<HttpClient> client = new Lazy<HttpClient>();
        private bool AllowTruncate => ConfigurationManager.AppSettings["AllowTruncateBookingDB"] == "true" ? true : false;
        private void TruncateDB(long from, long to)
        {
            try
            {
                using (var context = new BookingStaticDataEntities())
                {
                    // Remove Room Info
                    context.BulkDelete(context.RoomInfoes.Where(x => x.HotelId >= from && x.HotelId <= to));

                    // Remove Lat_Long
                    context.BulkDelete(context.HotelLatLongs.Where(x => x.HotelId >= from && x.HotelId <= to));

                    // Remove Locations
                    context.BulkDelete(context.Locations.Where(x => x.HotelId >= from && x.HotelId <= to));

                    // Remove Images
                    context.BulkDelete(context.ImgUrls.Where(x => x.HotelId >= from && x.HotelId <= to));

                    // Remove Facilities
                    context.BulkDelete(context.Facilities.Where(x => x.HotelId >= from && x.HotelId <= to));

                    // Remove GoodToKnow
                    context.BulkDelete(context.GoodToNows.Where(x => x.HotelId >= from && x.HotelId <= to));

                    // Remove Locations
                    context.BulkDelete(context.Locations.Where(x => x.HotelId >= from && x.HotelId <= to));

                    //Remove Logs
                    context.BulkDelete(context.logHotels.Where(x => x.HotelID >= from && x.HotelID <= to));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private UrlModel[] GetUrlRange(long from, long to)
        {
            try
            {
                using (var context = new BookingStaticDataEntities())
                {

                    context.Configuration.AutoDetectChangesEnabled = false;
                    return context.Hotels.AsNoTracking().Where(x => x.HotelId <= to && x.HotelId >= from && x.IsRecive == 2).Select(x => new UrlModel
                    {
                        ID = x.HotelId,
                        Url = x.hotel_url
                    }).ToArray();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private async Task<bool> InsertHotelAsync(long hotelID, HotelData data, byte isRecieve)
        {
            try
            {
                using (var context = new BookingStaticDataEntities())
                {
                    context.Configuration.AutoDetectChangesEnabled = false;
                    var hotel = context.Hotels.FirstOrDefault(x => x.HotelId == hotelID);
                    hotel.DescriptionSite = data.Description;
                    hotel.name = data.Name;
                    hotel.address = data.Address;
                    hotel.IsRecive = isRecieve;
                    hotel.LastUpdate = DateTime.Now;
                    context.GoodToNows.Add(new GoodToNow
                    {
                        CheckIn = data.GoodToKnow.CheckIn,
                        CheckOut = data.GoodToKnow.CheckOut,
                        HotelId = hotelID,
                        Pets = data.GoodToKnow.Pets,
                    });
                    context.HotelLatLongs.Add(new HotelLatLong
                    {
                        HotelId = hotelID,
                        Lat = data.Latitude,
                        Long = data.Longitude,
                    });
                    await context.BulkInsertAsync(data.HotelImageUrls.Select(x => new ImgUrl
                    {
                        HotelId = hotelID,
                        Path = x,
                        LastUpdate = DateTime.Now,
                    }).ToList());
                    await context.BulkInsertAsync(data.Locations.Select(x =>
                    {
                        x.HotelId = hotelID;
                        x.LastUpdate = DateTime.Now;
                        return x;
                    }).ToList());
                    await context.BulkInsertAsync(data.Facilities.Select(x =>
                    {
                        x.HotelId = hotelID;
                        x.LastUpdate = DateTime.Now;
                        return x;
                    }).ToList());
                    context.ChangeTracker.DetectChanges();
                    await context.BulkSaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                var stackTrace = new StackTrace();
                var frame = stackTrace.GetFrame(0);
                var methodName = frame.GetMethod().Name;
                await LogExceptionAsync(hotelID, ex, $"Method Name: {methodName} -- Insert Hotel Info to DB failed.");
                return false;
            }
        }

        private async Task<bool> InsertRoomAsync(long hotelID, List<RoomData> data)
        {
            try
            {
                var roomsInfo = new ConcurrentBag<RoomInfo>();
                Parallel.ForEach(data, roomData =>
                {
                    if (roomData.RoomImages.Count > 0)
                        roomData.RoomImages.ForEach(image =>
                        {
                            roomsInfo.Add(new RoomInfo
                            {
                                HotelId = hotelID,
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
                    else
                        roomsInfo.Add(new RoomInfo
                        {
                            HotelId = hotelID,
                            RoomSize = roomData.RoomSize,
                            RoomTypeIcon = roomData.Sleeps,
                            RoomTypeName = roomData.RoomType,
                            RoomTypeInfo = roomData.RoomTypeInfo,
                            RoomTypeIconInfo = roomData.SleepsInfo,
                            RoomFacilities = string.Join("-", roomData.Facilities),
                            RoomDescription = roomData.Description,
                            RoomTypeId = Convert.ToInt64(Regex.Replace(roomData.RoomTypeID, "[A-Za-z ]", "")),
                        });
                });
                using (var context = new BookingStaticDataEntities())
                {
                    await context.BulkInsertAsync(roomsInfo);
                }
                return true;
            }
            catch (Exception ex)
            {
                var stackTrace = new StackTrace();
                var frame = stackTrace.GetFrame(0);
                var methodName = frame.GetMethod().Name;
                await LogExceptionAsync(hotelID, ex, $"Method Name: {methodName} -- Insert Rooms to DB failed.");
                return false;
            }
        }

        private async Task LogExceptionAsync(long? hotelID, Exception exception, string comment)
        {
            try
            {
                using (var context = new BookingStaticDataEntities())
                {
                    context.logHotels.Add(new logHotel
                    {
                        HotelID = hotelID,
                        Exception = exception.Message + " -- " + exception.InnerException?.Message,
                        Description = DateTime.Now + " : " + comment,
                    });
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<BookingViewModel> GetAndSaveAsync(string url, long id, SemaphoreSlim semaphore)
        {
            await semaphore.WaitAsync();
            try
            {
                bool insertHotelToDB = false, insertRoomsToDB = false;
                (BookingResponseModel<HotelData> hotelRes, BookingResponseModel<List<RoomData>> roomRes) = await BookingAccess.GetDataAsync(client.Value, url);
                string message = string.Empty;
                byte isRecieve = (byte)(hotelRes.Success && roomRes.Success ? 1 : hotelRes.Success && !roomRes.Success ? 3 : 2);

                if (hotelRes.Success)
                {
                    if (await InsertHotelAsync(id, hotelRes.Data, isRecieve)) insertHotelToDB = true;
                }
                else
                {
                    message += "Get Hotel Info Message: ";
                    message += string.Join("--", hotelRes.Errors.Select(x => x.Text).ToArray());
                    if (hotelRes.Errors != null) await LogExceptionAsync(id, new Exception(string.Join(" ", hotelRes.Errors?.Select(x => x.Text).ToArray())), $"Method Name: GetHotelInfo -- Get Hotel Info from Booking failed.");
                }
                if (roomRes.Success)
                {
                    if (await InsertRoomAsync(id, roomRes.Data)) insertRoomsToDB = true;
                }
                else
                {
                    message += "Get Room Info Message: ";
                    message += string.Join("--", roomRes.Errors.Select(x => x.Text).ToArray());
                    if (roomRes.Errors != null) await LogExceptionAsync(id, new Exception(string.Join(" ", roomRes.Errors?.Select(x => x.Text).ToArray())), $"Method Name: GetRooms -- Get Rooms Info from Booking failed.");
                }
                return new BookingViewModel
                {
                    HotelID = id,
                    GetHotelInfo = hotelRes.Success,
                    GetRoomInfo = roomRes.Success,
                    InsertHotelToDB = insertHotelToDB,
                    InsertRoomsToDB = insertRoomsToDB,
                    Message = message
                };

            }
            catch (Exception ex)
            {
                await LogExceptionAsync(id, ex, "");
                return new BookingViewModel
                {
                    HotelID = id,
                    Message = ex.Message
                };
            }
            finally
            {
                semaphore.Release();
            }
        }

        public async Task<BookingViewModel[]> MapBookingInfoAsync(long from, long to)
        {
            try
            {
                if (from > to) throw new ArgumentOutOfRangeException();
                if (AllowTruncate) TruncateDB(from, to);
                ServicePointManager.DefaultConnectionLimit = 50;
                UrlModel[] urls = GetUrlRange(from, to);
                using (var semaphor = new SemaphoreSlim(50))
                {
                    Task<BookingViewModel>[] tasks = urls.Select(x => GetAndSaveAsync(x.Url.Remove(x.Url.LastIndexOf("?")), x.ID, semaphor)).ToArray();
                    return await Task.WhenAll(tasks);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
