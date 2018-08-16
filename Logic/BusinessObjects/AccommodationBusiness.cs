using Data;
using Data.DataModel;
using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Get Full Name Of Accommodations
        /// </summary>
        /// <returns></returns>
        public List<AccommodationModels.ListNameAccommodation> GetNames()
        {
            try
            {
                return DataContext.Context.Accommodations.AsNoTracking().ToList().Select(x => new AccommodationModels.ListNameAccommodation
                {
                    AccommodationlID = x.AccommodationlID,
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
    }
}
