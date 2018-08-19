using Data;
using Data.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
/// <summary>
/// Business Object
/// </summary>
namespace Logic.BusinessObjects
{
    /// <summary>
    /// Pubic Objects Of DataBase
    /// </summary>
    public class PublicBusiness
    {
        /// <summary>
        /// Get List Of Locations
        /// </summary>
        /// <returns></returns>
        public List<Location> Locations()
        {
            try
            {
                return DataContext.Context.Locations.ToList();
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }
        /// <summary>
        /// Get List Of Airports
        /// </summary>
        /// <returns></returns>
        public List<Airport> Airports()
        {
            try
            {
                return DataContext.Context.Airports.ToList();
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }
        /// <summary>
        /// Get List Of Languages
        /// </summary>
        /// <returns></returns>
        public List<Language> Languages()
        {
            try
            {
                return DataContext.Context.Languages.ToList();
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }
        /// <summary>
        /// Get List Of Currencies
        /// </summary>
        /// <returns></returns>
        public List<Currency> Currencies()
        {
            try
            {
                return DataContext.Context.Currencies.ToList();
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }
        public User1 GetUser(object userID)
        {
            if (userID == null)
                return null;
            long userIDLong = Convert.ToInt64(userID);
            return DataContext.Context.User1.SingleOrDefault(p => p.UserID == userIDLong);
        }
        public Currency GetCurrency(long ID)
        {
            try
            {
                return DataContext.Context.Currencies.Where(x => x.CurrencyID == ID).FirstOrDefault();
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }
        public Language GetLanguage(long ID)
        {
            try
            {
                return DataContext.Context.Languages.Where(x => x.LanguageID == ID).FirstOrDefault();
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }
    }
}
