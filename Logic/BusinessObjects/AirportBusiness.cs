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
    public class AirportBusiness
    {
        /// <summary>
        /// Create Class Public
        /// </summary>
        PublicBusiness publicBusiness = new PublicBusiness();
        /// <summary>
        /// Get All Airports
        /// </summary>
        /// <returns>Airports</returns>
        public List<Airport> GetAll()
        {
            try
            {
                return publicBusiness.Airports();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
        /// <summary>
        /// Search Advance Airport
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public List<Airport> Search(AirportModels.SearchAirport Model)
        {
            try
            {
                List<Airport> Query = new List<Airport>();
                if (Model.AirportID != null)
                {
                    if (Query.Count == 0)
                    {
                        Query = DataContext.Context.Airports.Where(x => x.AirportID == Model.AirportID).ToList();
                    }
                    else
                    {
                        Query = Query.Where(x => x.AirportID == Model.AirportID).ToList();
                    }

                }
                if (Model.LocationID != null)
                {
                    if (Query.Count == 0)
                    {
                        Query = DataContext.Context.Airports.Where(x => x.LocationID == Model.LocationID).ToList();
                    }
                    else
                    {
                        Query = Query.Where(x => x.LocationID == Model.LocationID).ToList();
                    }

                }
                if (!String.IsNullOrEmpty(Model.Name))
                {
                    if (Query.Count == 0)
                    {
                        Query = DataContext.Context.Airports.Where(x => x.Name.ToLower().Contains(Model.Name.ToLower())).ToList();
                    }
                    else
                    {
                        Query = Query.Where(x => x.Name.ToLower().Contains(Model.Name.ToLower())).ToList();
                    }

                }
                if (!String.IsNullOrEmpty(Model.Code))
                {
                    if (Query.Count == 0)
                    {
                        Query = DataContext.Context.Airports.Where(x => x.Code.ToLower().Contains(Model.Code.ToLower())).ToList();
                    }
                    else
                    {
                        Query = Query.Where(x => x.Code.ToLower().Contains(Model.Code.ToLower())).ToList();
                    }

                }                
                return Query;
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}
