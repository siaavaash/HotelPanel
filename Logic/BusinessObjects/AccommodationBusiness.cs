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
using static Data.ViewModel.AccommodationModels;

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
        public List<AccommodationModels.ListNameAccommodation> GetNames(AccommodationModels.SearchAccommodation Model)
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
                if (Model.Verified != true && Query.Count != 0)
                {
                    Query = Query.Where(x => x.IsVerified != true).ToList();
                }
                return Query.Select(x => new AccommodationModels.ListNameAccommodation
                {
                    AccommodationID = x.AccommodationlID,
                    Name = x.Name,
                    CityName = x.CityName,
                    Country = x.Country,
                    lastUpdate = x.lastUpdate
                }).ToList();
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
                var Verified = DataContext.Context.Accommodations.Where(x => x.AccommodationlID == AccommodationID).FirstOrDefault().IsVerified;
                if (!(bool)Verified)
                {
                    return DataContext.Context.AccomodationImages.Where(x => x.AccommodationlID == AccommodationID && (x.IsActive ?? false)).ToList();
                }
                else
                {
                    return DataContext.Context.AccomodationImages.Where(x => x.AccommodationlID == AccommodationID).ToList();
                }

            }
            catch (Exception exeption)
            {
                throw exeption;
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
        public bool EditName(AccommodationModels.EditName Model)
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
        public bool EditDescription(AccommodationModels.EditDescription Model)
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
        public bool AddFacilities(AccommodationModels.AddFacilities Model)
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
        public bool EditFacilities(AccommodationModels.EditFacilities Model)
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
        public bool AddDescription(AccommodationModels.AddDescription Model)
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
        public bool ChangeDescription(AccommodationModels.ChangeDescription Model)
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
                return new RoomImagesViewModel
                {
                    AccommodationID = accommodationId,
                    RoomImages = DataContext.Context.AccomodationRoomImages.Where(x => x.AccommodationID == accommodationId).ToList()
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool VerifyRoomImages(RoomImagesViewModel roomImages)
        {
            try
            {
                foreach (var roomImage in roomImages.RoomImages ?? new List<AccomodationRoomImage>())
                {
                    var image = DataContext.Context.AccomodationRoomImages.FirstOrDefault(x => x.AccomodationRoomImageID == roomImage.AccomodationRoomImageID);
                    if (image != null)
                    {
                        image.IsVerified = true;
                        image.IsReported = roomImage.IsReported;
                        image.IsActive = roomImage.IsActive;
                        image.VerifiedDate = DateTime.Now.Date;
                    }
                }
                return DataContext.Context.SaveChanges() > 0 ? true : false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
