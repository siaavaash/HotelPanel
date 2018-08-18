using Data.ViewModel;
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
        [HttpGet]
        public ActionResult List(AccommodationModels.SearchAccommodation Model)
        {
            if(ModelState.IsValid)
            {
                var Result = _AccommodationBusiness.GetNames(Model);
                return View(Result);
            }
            return View(new List<Data.ViewModel.AccommodationModels.ListNameAccommodation>());
        }
        public ActionResult Images(long AccommodationID)
        {
            var Model = _AccommodationBusiness.GetImages(AccommodationID);
            return View(Model);
        }
        [HttpPost]
        public ActionResult Filter(List<long> ImageID)
        {
            bool Result = true;//_AccommodationBusiness.FilterImages(ImageID);
            if (Result)
            {
                Session["Message"] = "Your Accommodation Images Successfully Filtered";
                return RedirectToAction("List");
            }
            return RedirectToAction("List");
        }
    }
}