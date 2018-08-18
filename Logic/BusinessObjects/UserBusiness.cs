using Data;
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
        public Boolean LoginUser(LoginModels.LoginEntry model)
        {
            try
            {
                var user = DataContext.Context.Users.FirstOrDefault(i => i.Username == model.Username && i.Password == model.Password);
                if (user != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception xx)
            {

                throw xx;
            }
        }
    }
}
