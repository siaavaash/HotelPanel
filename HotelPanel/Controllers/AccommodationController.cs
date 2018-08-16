using Logic.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelPanel.Controllers
{
    public class AccommodationController : Controller
    {
        AccommodationBusiness _AccommodationBusiness = new AccommodationBusiness();
        // GET: Accommodation
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            var Model = _AccommodationBusiness.GetNames();
            return View(Model);
        }
    }
}