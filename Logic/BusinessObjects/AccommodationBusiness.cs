using Data;
using Data.DataModel;
using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using Common;
using static Data.ViewModel.WeatherModels.Forecast;

namespace Logic.BusinessObjects
{
    public class AccommodationBusiness
    {
        /// <summary>
        /// Get All Accommodation List
        /// </summary>
        /// <returns></returns>
        public List<Accommodation> GetAll()
        {
            try
            {
                return DataContext.Context.accommodationimages.ToList();
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
                return DataContext.Context.accommodationimages.Where(x => x.AccommodationlID == AccommodationID).FirstOrDefault();
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
                        Query = DataContext.Context.accommodationimages.Where(x => x.AccommodationlID == Model.AccommodationID).ToList();
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
                        Query = DataContext.Context.accommodationimages.Where(x => x.Name.ToLower().Contains(Model.Name.ToLower())).ToList();
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
                        Query = DataContext.Context.accommodationimages.Where(x => x.CityName.ToLower().Contains(Model.CityName.ToLower())).ToList();
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
                        Query = DataContext.Context.accommodationimages.Where(x => x.Country.ToLower().Contains(Model.Country.ToLower())).ToList();
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
                        Query = DataContext.Context.accommodationimages.Where(x => x.AccommodationlID >= Model.From).ToList();
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
                        Query = DataContext.Context.accommodationimages.Where(x => x.AccommodationlID <= Model.To).ToList();
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
                var Verified = DataContext.Context.accommodationimages.Where(x => x.AccommodationlID == AccommodationID).FirstOrDefault().IsVerified;
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
                return DataContext.Context.accommodationimages.Where(x => x.AccommodationlID == AccommodationID).FirstOrDefault().Facilities.ToList();
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
                return DataContext.Context.accommodationimages.Where(x => x.AccommodationlID == AccommodationID).FirstOrDefault().Description;
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
                var Accommodation = Data.DataContext.Context.accommodationimages.Where(x => x.AccommodationlID == AccommodationID).FirstOrDefault();
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
                var Accommodations = DataContext.Context.accommodationimages.Where(x => x.AccommodationlID == Model.AccommodationID).FirstOrDefault();
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
                var Accommodations = DataContext.Context.accommodationimages.Where(x => x.AccommodationlID == Model.AccommodationID).FirstOrDefault();
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
                var Accommodations = DataContext.Context.accommodationimages.Where(x => x.AccommodationlID == Model.AccommodationID).FirstOrDefault();
                foreach(var Item in Model.FacilityID)
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
                var Accommodations = DataContext.Context.accommodationimages.Where(x => x.AccommodationlID == Model.AccommodationID).FirstOrDefault();
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
    }
}
