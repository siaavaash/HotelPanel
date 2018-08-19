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
                return DataContext.Context.Accommodations.ToList();
            }
            catch(Exception exeption)
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
                    if(Query.Count == 0)
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
                return Query.Select(x => new AccommodationModels.ListNameAccommodation
                {
                    AccommodationID = x.AccommodationlID,
                    Name = x.Name,
                    CityName=x.CityName,
                    Country=x.Country,
                    lastUpdate=x.lastUpdate
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
                return DataContext.Context.AccomodationImages.Where(x => x.AccommodationlID==AccommodationID).ToList();
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
                Result.Google = new Data.PublicModel.LocationModels.Google
                {
                    Lat = Accommodation.Latitude,
                    Lng = Accommodation.Longitude
                };
                return Result;
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
                foreach(var Item in ImageID)
                {
                    var Model = DataContext.Context.AccomodationImages.Where(x => x.ImageID == Item).FirstOrDefault();
                    if(Model != null)
                    {
                        Model.IsActive = false;
                        Model.DateActive = DateTime.Now;
                        //Model.UserID = ;
                        DataContext.Context.Entry(Model).State = EntityState.Modified;
                    }                    
                }
                return true;
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }
    }
}
