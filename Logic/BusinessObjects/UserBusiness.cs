using Data;
using Data.DataModel;
using Data.PublicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.BusinessObjects
{
    public class UserBusiness
    {
        /// <summary>
        /// لاگین یوزر
        /// </summary>
        /// <returns></returns>
        public User1 LoginUser(LoginModels.LoginEntry model)
        {
            try
            {
                var user = DataContext.Context.User1.Where(i => i.Username == model.Username && i.Password == model.Password).FirstOrDefault();
                if (user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception xx)
            {

                throw xx;
            }
        }
    }
}
