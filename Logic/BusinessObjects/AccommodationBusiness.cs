using Data;
using Data.DataModel;
using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Service;
using Common;
using static Data.ViewModel.WeatherModels.Forecast;
using Data.PublicModel;
using Data.ViewModel.AccommodationModels;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections;

namespace Logic.BusinessObjects
{
    public class AccommodationBusiness
    {
        public List<string> GetCountries(string query)
        {
            try
            {
                return DataContext.Context.Locations.Where(x => x.LocationTypeID == 2 && x.Name.StartsWith(query)).OrderBy(x => x.Name).Take(20).Select(x => x.Name).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<string> GetCities(string query)
        {
            try
            {
                return DataContext.Context.Locations.Where(x => x.LocationTypeID == 4 && x.Name.StartsWith(query)).OrderBy(x => x.Name).Take(20).Select(x => x.Name).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Get All Accommodation List
        /// </summary>
        /// <returns></returns>
        public List<Accommodation> GetAll()
        {
            try
            {
                return DataContext.Context.Accommodations.ToList();
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }
        public AccommodationListViewModel GetAccommodationByUser(long userId, bool showIsVerified, bool onlyVerified)
        {
            try
            {
                using (var context = new Entities())
                {
                    context.Configuration.AutoDetectChangesEnabled = false;
                    var userBounds = context.UserPictureDics.AsNoTracking().Where(x => x.UserID == userId && x.FromImageID != 0 && x.ToImageID != 0).ToList();
                    if (userBounds != null && userBounds.Count > 0)
                    {
                        var result = new AccommodationListViewModel
                        {
                            Restricted = true,
                            AccommodationList = new List<AccommodationListRsult>(),
                        };
                        if (onlyVerified)
                        {
                            foreach (var bound in userBounds)
                                result.AccommodationList.AddRange(context.Accommodations.AsNoTracking().Include(x => x.AccommodationSortedByCountry).Where(x => x.AccommodationSortedByCountry.Ordered >= bound.FromImageID && x.AccommodationSortedByCountry.Ordered <= bound.ToImageID && x.DateVerified != null).Select(x => new AccommodationListRsult
                                {
                                    AccommodationID = x.AccommodationlID,
                                    CityName = x.CityName,
                                    Country = x.Country,
                                    lastUpdate = x.lastUpdate,
                                    Name = x.Name
                                }).ToList());
                        }
                        else
                        {
                            if (!showIsVerified)
                            {
                                foreach (var bound in userBounds)
                                    result.AccommodationList.AddRange(context.Accommodations.AsNoTracking().Include(x => x.AccommodationSortedByCountry).Where(x => x.AccommodationSortedByCountry.Ordered >= bound.FromImageID && x.AccommodationSortedByCountry.Ordered <= bound.ToImageID && x.DateVerified == null).Select(x => new AccommodationListRsult
                                    {
                                        AccommodationID = x.AccommodationlID,
                                        CityName = x.CityName,
                                        Country = x.Country,
                                        lastUpdate = x.lastUpdate,
                                        Name = x.Name
                                    }).ToList());
                            }
                            else
                            {
                                foreach (var bound in userBounds)
                                    result.AccommodationList.AddRange(context.Accommodations.AsNoTracking().Include(x => x.AccommodationSortedByCountry).Where(x => x.AccommodationSortedByCountry.Ordered >= bound.FromImageID && x.AccommodationSortedByCountry.Ordered <= bound.ToImageID && x.DateVerified == null).Select(x => new AccommodationListRsult
                                    {
                                        AccommodationID = x.AccommodationlID,
                                        CityName = x.CityName,
                                        Country = x.Country,
                                        lastUpdate = x.lastUpdate,
                                        Name = x.Name
                                    }).ToList());
                            }
                        }
                        result.AccommodationList = result.AccommodationList.Distinct(new AccommodationEquality<AccommodationListRsult>(x => x.AccommodationID)).OrderBy(x => x.AccommodationID).ToList();
                        return result;
                    }
                    return new AccommodationListViewModel
                    {
                        Restricted = false
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Get Accommodation By ID
        /// </summary>
        /// <param name="AccommodationID"></param>
        /// <returns></returns>
        public Accommodation GetAccommodation(long AccommodationID)
        {
            try
            {
                return DataContext.Context.Accommodations.Where(x => x.AccommodationlID == AccommodationID).FirstOrDefault();
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }
        /// <summary>
        /// Get Full Name Of Accommodations
        /// </summary>
        /// <returns></returns>
        public AccommodationListViewModel GetNames(SearchAccommodation Model)
        {
            try
            {
                List<Accommodation> Query = new List<Accommodation>();
                if (Model.AccommodationID != null)
                {
                    if (Query.Count == 0)
                    {
                        Query = DataContext.Context.Accommodations.Where(x => x.AccommodationlID == Model.AccommodationID).ToList();
                    }
                    else
                    {
                        Query = Query.Where(x => x.AccommodationlID == Model.AccommodationID).ToList();
                    }

                }
                if (!String.IsNullOrEmpty(Model.Name))
                {
                    if (Query.Count == 0)
                    {
                        Query = DataContext.Context.Accommodations.Where(x => x.Name.ToLower().Contains(Model.Name.ToLower())).ToList();
                    }
                    else
                    {
                        Query = Query.Where(x => x.Name.ToLower().Contains(Model.Name.ToLower())).ToList();
                    }

                }
                if (!String.IsNullOrEmpty(Model.CityName))
                {
                    if (Query.Count == 0)
                    {
                        Query = DataContext.Context.Accommodations.Where(x => x.CityName.ToLower().Contains(Model.CityName.ToLower())).ToList();
                    }
                    else
                    {
                        Query = Query.Where(x => x.CityName.ToLower().Contains(Model.CityName.ToLower())).ToList();
                    }

                }
                if (!String.IsNullOrEmpty(Model.Country))
                {
                    if (Query.Count == 0)
                    {
                        Query = DataContext.Context.Accommodations.Where(x => x.Country.ToLower().Contains(Model.Country.ToLower())).ToList();
                    }
                    else
                    {
                        Query = Query.Where(x => x.Country.ToLower().Contains(Model.Country.ToLower())).ToList();
                    }

                }
                if (Model.From != null)
                {
                    if (Query.Count == 0)
                    {
                        Query = DataContext.Context.Accommodations.Where(x => x.AccommodationlID >= Model.From).ToList();
                    }
                    else
                    {
                        Query = Query.Where(x => x.AccommodationlID >= Model.From).ToList();
                    }

                }
                if (Model.To != null)
                {
                    if (Query.Count == 0)
                    {
                        Query = DataContext.Context.Accommodations.Where(x => x.AccommodationlID <= Model.To).ToList();
                    }
                    else
                    {
                        Query = Query.Where(x => x.AccommodationlID <= Model.To).ToList();
                    }

                }
                if (Model.OnlyVerified)
                {
                    Query = Query.Where(x => x.DateVerified != null).ToList();
                }
                else
                {
                    if (!Model.Verified)
                    {
                        Query = Query.Where(x => x.DateVerified == null).ToList();
                    }
                }
                var result = new AccommodationListViewModel
                {
                    AccommodationList = Query.Select(x => new AccommodationListRsult
                    {
                        AccommodationID = x.AccommodationlID,
                        Name = x.Name,
                        CityName = x.CityName,
                        Country = x.Country,
                        lastUpdate = x.lastUpdate
                    }).ToList(),
                };
                return result;
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }
        /// <summary>
        /// Get Accommodation Images By ID
        /// </summary>
        /// <param name="AccommodationID"></param>
        /// <returns></returns>
        public List<AccomodationImage> GetImages(long AccommodationID)
        {
            try
            {
                return DataContext.Context.AccomodationImages.Where(x => x.AccommodationlID == AccommodationID).ToList().Select(x => { x.Link = x.Link.Replace("../", "/"); return x; }).ToList();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public List<Facility> GetFacilities(long AccommodationID)
        {
            try
            {
                return DataContext.Context.Accommodations.Where(x => x.AccommodationlID == AccommodationID).FirstOrDefault().Facilities.ToList();
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }
        public string GetDescription(long AccommodationID)
        {
            try
            {
                return DataContext.Context.Accommodations.Where(x => x.AccommodationlID == AccommodationID).FirstOrDefault().Description;
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }
        public WeatherModels.Forecast.Root GetWeather(long AccommodationID)
        {
            try
            {
                var Accommodation = GetAccommodation(AccommodationID);
                var Result = WeatherConcreteMapper.Avail(Accommodation.CityName);
                WeatherModels.Forecast.Root Model = new WeatherModels.Forecast.Root();
                Model.Forecast = new List<ForecastDays>();
                Model.Wind = Result.query.results.channel.wind.speed.ToKM().ToString();
                Model.Sunrise = Result.query.results.channel.astronomy.sunrise.ToUpper();
                Model.Pressure = Result.query.results.channel.atmosphere.pressure;
                Model.Google = new Data.PublicModel.LocationModels.Google
                {
                    Lat = Accommodation.Latitude,
                    Lng = Accommodation.Longitude
                };
                for (int i = 0; i < 7; i++)
                {
                    Model.Forecast.Add(new ForecastDays
                    {
                        Date = Result.query.results.channel.item.forecast[i].date,
                        Code = (int.Parse(Result.query.results.channel.item.forecast[i].code)),
                        Day = Result.query.results.channel.item.forecast[i].day,
                        High = Utility.FahrenheitToCentigrade(Result.query.results.channel.item.forecast[i].high).ToString(),
                        Low = Utility.FahrenheitToCentigrade(Result.query.results.channel.item.forecast[i].low).ToString(),
                        Text = Result.query.results.channel.item.forecast[i].text
                    });
                }
                return Model;
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }
        public bool FilterImages(List<long> ImageID)
        {
            try
            {
                foreach (var Item in ImageID)
                {
                    var Model = DataContext.Context.AccomodationImages.Where(x => x.ImageID == Item).FirstOrDefault();
                    if (Model != null)
                    {
                        Model.IsActive = false;
                        Model.DateActive = DateTime.Now;
                        DataContext.Context.Entry(Model).State = EntityState.Modified;
                    }
                    DataContext.Context.SaveChanges();
                }
                return true;
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }
        public bool ReloadVerify(long AccommodationID)
        {
            try
            {
                var Accommodation = Data.DataContext.Context.AccomodationImages.Where(x => x.AccommodationlID == AccommodationID).ToList();
                foreach (var Item in Accommodation)
                {
                    Item.IsActive = true;
                    DataContext.Context.Entry(Item).State = EntityState.Modified;
                }
                DataContext.Context.SaveChanges();
                return true;
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }
        public bool Verify(long AccommodationID)
        {
            try
            {
                var Accommodation = Data.DataContext.Context.Accommodations.Where(x => x.AccommodationlID == AccommodationID).FirstOrDefault();
                Accommodation.IsVerified = true;
                DataContext.Context.SaveChanges();
                return true;
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }
        public bool EditName(EditName Model)
        {
            try
            {
                var Accommodations = DataContext.Context.Accommodations.Where(x => x.AccommodationlID == Model.AccommodationID).FirstOrDefault();
                if (Accommodations != null)
                {
                    Accommodations.Name = Model.Name;
                    DataContext.Context.Entry(Accommodations).State = EntityState.Modified;
                    DataContext.Context.SaveChanges();
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }
        public bool EditDescription(EditDescription Model)
        {
            try
            {
                var Accommodations = DataContext.Context.Accommodations.Where(x => x.AccommodationlID == Model.AccommodationID).FirstOrDefault();
                if (Accommodations != null)
                {
                    Accommodations.Description = Model.Description;
                    DataContext.Context.Entry(Accommodations).State = EntityState.Modified;
                    DataContext.Context.SaveChanges();
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }
        public bool AddFacilities(AddFacilities Model)
        {
            try
            {
                var Accommodations = DataContext.Context.Accommodations.Where(x => x.AccommodationlID == Model.AccommodationID).FirstOrDefault();
                foreach (var Item in Model.FacilityID)
                {
                    var Facility = DataContext.Context.Facilities.Where(x => x.FacilityID == Item).FirstOrDefault();
                    Accommodations.Facilities.Add(Facility);
                    DataContext.Context.SaveChanges();
                }
                return true;
            }
            catch
            {
            }
            return false;
        }
        public bool EditFacilities(EditFacilities Model)
        {
            try
            {
                var Accommodations = DataContext.Context.Accommodations.Where(x => x.AccommodationlID == Model.AccommodationID).FirstOrDefault();
                if (Accommodations != null)
                {
                    var Facility = Accommodations.Facilities.Where(x => x.FacilityID == Model.FacilityID).FirstOrDefault();
                    Facility.Name = Model.Name;
                    DataContext.Context.SaveChanges();
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }
        public List<Facility> GetFacilities()
        {
            try
            {
                return DataContext.Context.Facilities.ToList();
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }
        public AccommodationDescription GetAccommodationDescription(long AccommodationDescriptionID)
        {
            try
            {
                return DataContext.Context.AccommodationDescriptions.Where(x => x.AccommodationDescriptionID == AccommodationDescriptionID).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }
        public bool AddDescription(AddDescription Model)
        {
            try
            {
                bool Duplicate = DataContext.Context.AccommodationDescriptions.Any(x => x.AccommodationID == Model.AccommodationID && x.LanguageID == Model.LanguageID);
                if (Duplicate)
                    return false;
                var Entry = new AccommodationDescription()
                {
                    AccommodationID = Model.AccommodationID,
                    ModificationTime = DateTime.Now,
                    ModifyUserID = Model.ModifyUserID,
                    UserID = Model.UserID,
                    CreationTime = DateTime.Now,
                    Description = Model.Description,
                    LanguageID = Model.LanguageID,
                };
                DataContext.Context.AccommodationDescriptions.Add(Entry);
                DataContext.Context.SaveChanges();
                return true;
            }
            catch
            {
            }
            return false;
        }
        public bool ChangeDescription(ChangeDescription Model)
        {
            try
            {
                var Entry = DataContext.Context.AccommodationDescriptions.Where(x => x.AccommodationDescriptionID == Model.AccommodationDescriptionID).FirstOrDefault();
                Entry.AccommodationID = Model.AccommodationID;
                Entry.Description = Model.Description;
                Entry.ModificationTime = DateTime.Now;
                Entry.ModifyUserID = Model.ModifyUserID;
                Entry.LanguageID = Model.LanguageID;
                DataContext.Context.Entry(Entry).State = EntityState.Modified;
                DataContext.Context.SaveChanges();
                return true;
            }
            catch
            {
            }
            return false;
        }
        public List<AccommodationDescription> ListOtherLanguageDescription(long AccommodationID)
        {
            try
            {
                var Result = DataContext.Context.AccommodationDescriptions.Where(x => x.AccommodationID == AccommodationID).ToList();
                return Result;
            }
            catch
            {
            }
            return new List<AccommodationDescription>();
        }
        /// <summary>
        /// Get Top 4 Most Search Results Location
        /// </summary>
        /// <returns></returns>
        public List<Data.PublicModel.AccommodationModels.SearchReport> GetSearchReport()
        {
            try
            {
                List<Data.PublicModel.AccommodationModels.SearchReport> Result = new List<Data.PublicModel.AccommodationModels.SearchReport>();
                var Data = DataContext.Context.GetHotelAvailInfoes.AsNoTracking().AsParallel().ToList();
                var DistincData = Data.Select(x => x.LocationID).Distinct().ToList();
                foreach (var Item in DistincData)
                {
                    Data.PublicModel.AccommodationModels.SearchReport Entry = new Data.PublicModel.AccommodationModels.SearchReport();
                    Entry.LocationID = Item.Value;
                    Entry.Count = Data.Where(x => x.LocationID == Item.Value).Count();
                    Result.Add(Entry);
                }
                Result = Result.OrderByDescending(x => x.Count).Take(4).ToList();
                var Total = Result.Select(x => x.Count).Sum();
                foreach (var _Item in Result)
                {
                    var Locations = DataContext.Context.Locations.Find(_Item.LocationID);
                    _Item.City = Locations.Name;
                    _Item.Country = DataContext.Context.Locations.Where(x => x.CityId == Locations.ParentLocationID).FirstOrDefault().Name;
                    _Item.Percent = (_Item.Count * 100) / Total;
                }

                return Result;
            }
            catch (Exception ex)
            {

            }
            return new List<Data.PublicModel.AccommodationModels.SearchReport>();
        }
        public RoomImagesViewModel GetRoomImageByAccId(long accommodationId)
        {
            try
            {
                using (var context = new Entities())
                {
                    context.Configuration.AutoDetectChangesEnabled = false;
                    return new RoomImagesViewModel
                    {
                        AccommodationID = accommodationId,
                        RoomImages = context.AccomodationRoomImages.Where(x => x.AccommodationID == accommodationId).ToList()
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool VerifyRoomImages(long userId, Data.ViewModel.AccommodationModels.RoomImagesViewModel roomImages)
        {
            var savedItems = 0;
            try
            {
                //foreach (var roomImage in roomImages.RoomImages ?? new List<AccomodationRoomImage>())
                Parallel.ForEach(roomImages.RoomImages ?? new List<AccomodationRoomImage>(), roomImage =>
                 {
                     using (var context = new Entities())
                     {
                         var image = context.AccomodationRoomImages.FirstOrDefault(x => x.AccomodationRoomImageID == roomImage.AccomodationRoomImageID);
                         var accommodation = context.Accommodations.First(x => x.AccommodationlID == image.AccommodationID);
                         accommodation.IsVerified = true;
                         accommodation.DateVerified = DateTime.Now;
                         accommodation.UserID = userId;
                         if (image != null)
                         {
                             image.IsVerified = true;
                             image.IsReported = roomImage.IsReported ?? image.IsReported;
                             image.IsActive = roomImage.IsActive ?? image.IsActive;
                             image.VerifiedDate = DateTime.Now;
                             image.UserID = userId;
                         }
                         savedItems += context.SaveChanges();
                     }
                 });
                return savedItems > 0 ? true : false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool VerifyAccommodationImages(long userId, FilterImagesView model)
        {
            var savedItems = 0;
            try
            {
                //foreach (var accImage in model.AccomodationImages ?? new List<AccomodationImage>())
                Parallel.ForEach(model.AccomodationImages ?? new List<AccomodationImage>(), accImage =>
                {
                    using (var context = new Entities())
                    {
                        var image = context.AccomodationImages.FirstOrDefault(x => x.ImageID == accImage.ImageID);
                        var accommodation = context.Accommodations.First(x => x.AccommodationlID == image.AccommodationlID);
                        accommodation.IsVerified = true;
                        accommodation.DateVerified = DateTime.Now;
                        accommodation.UserID = userId;
                        if (image != null)
                        {
                            image.IsVerified = true;
                            image.IsReported = accImage.IsReported ?? image.IsReported;
                            image.IsActive = accImage.IsActive ?? image.IsActive;
                            image.VerifiedDate = DateTime.Now;
                            image.UserID = userId;
                        }
                        savedItems += context.SaveChanges();
                    }
                });
                return savedItems > 0 ? true : false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Dictionary<string, List<AccomodationRoomImage>> GetGroupedRoomImage(long accommodationId)
        {
            try
            {
                var images = new List<AccomodationRoomImage>();
                using (var context = new Entities())
                {
                    images = context.AccomodationRoomImages.Where(x => x.AccommodationID == accommodationId).ToList();
                }
                var result = new Dictionary<string, List<AccomodationRoomImage>>();
                foreach (var image in images)
                {
                    var thisTitle = result.FirstOrDefault(x => x.Key == image.RoomTypeName);
                    if (thisTitle.Key == null)
                    {
                        result.Add(image.RoomTypeName, new List<AccomodationRoomImage>
                        {
                            image
                        });
                    }
                    else
                        thisTitle.Value.Add(image);
                }
                return result.Keys.OrderBy(x => x).ToDictionary(x => x, x => result[x]);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Dictionary<string, List<AccomodationImage>> GetMultipleHotelImage(List<int> ids)
        {
            try
            {
                var result = new Dictionary<string, List<AccomodationImage>>();
                Parallel.ForEach(ids, id =>
                {
                    using (var context = new Entities())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false;
                        var hotel = context.Accommodations.AsNoTracking().FirstOrDefault(x => x.AccommodationlID == id);
                        var name = $"{hotel.Name} - {hotel.AccommodationlID}";
                        result.Add(name, context.AccomodationImages.Where(x => x.AccommodationlID == id).OrderBy(x => x.ImageID).ToList() ?? new List<AccomodationImage>());
                    }
                });
                return result.Keys.OrderBy(x => x).ToDictionary(x => x, x => result[x]);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<MultipleRoomsImageViewModel> GetMultipleRoomsImages(List<int> ids)
        {
            try
            {
                var result = new List<MultipleRoomsImageViewModel>();
                Parallel.ForEach(ids, id =>
                {
                    string hotelName = "";
                    using (var context = new Entities())
                    {
                        hotelName = context.Accommodations.FirstOrDefault(x => x.AccommodationlID == id)?.Name;
                    }
                    result.Add(new MultipleRoomsImageViewModel
                    {
                        Name = hotelName,
                        AccommodationID = id,
                        GroupedImages = GetGroupedRoomImage(id)
                    });
                });
                return result.OrderBy(x => x.Name).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    public class AccommodationEquality<T> : IEqualityComparer<T> where T : class
    {
        private Func<T, long> func;
        public AccommodationEquality(Func<T, long> func)
        {
            this.func = func;
        }

        public bool Equals(T x, T y)
        {
            long first = func(x);
            long second = func(y);
            if (first == second) return true;
            return false;
        }

        public int GetHashCode(T obj)
        {
            return func(obj).GetHashCode();
        }
    }
}
