using Data.PublicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelPanel.Controllers
{
    public class LoginController : Controller
    {
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
            return View();
        }
    }
}