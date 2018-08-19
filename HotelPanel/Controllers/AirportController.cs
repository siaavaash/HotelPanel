using Data.ViewModel;
using Logic.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelPanel.Controllers
{
    public class AirportController : BaseController
    {
        AirportBusiness airportBusiness = new AirportBusiness();
        // GET: Airport
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult List()
        {
            List<Data.DataModel.Airport> Result = new List<Data.DataModel.Airport>();
            return View(Result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult List(AirportModels.SearchAirport Model)
        {
            var Result = airportBusiness.Search(Model);
            return View(Result);
        }
    }
}