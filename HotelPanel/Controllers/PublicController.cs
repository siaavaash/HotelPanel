using Data.PublicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelPanel.Controllers
{
    public class PublicController : Controller
    {
        // GET: Public
        public ActionResult Index(LocationModels.Google Model)
        {
            return View(Model);
        }
    }
}