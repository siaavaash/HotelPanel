using Common;
using Data.PublicModel;
using Logic.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelPanel.Controllers
{
    public class LoginController : Controller
    {
        UserBusiness userBusiness = new UserBusiness();
        /// <summary>
        /// Login View
        /// </summary>
        /// <returns>View Login</returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Login Post Data
        /// </summary>
        /// <param name="Model">LoginEntry</param>
        /// <returns>View Admin Panel If Login Result Success</returns>
        [HttpPost]
        public ActionResult Index(LoginModels.LoginEntry Model)
        {
            if (Model.Username == null && Model.Password == null)
            {
                Session["Message-Error"] = "Please enter username and password";
                return Redirect(Constants.URL.Routes.Login);
            }
            Model.Password = Model.Password.ToMD5();
            Boolean resultLogin = userBusiness.LoginUser(Model);
            if (resultLogin)
            {
                return Redirect(Constants.URL.Routes.Home);
            }
            else
            {
                Session["Message-Error"] = "Password or username is not correct";
                return Redirect(Constants.URL.Routes.Login);
            }
        }
    }
}