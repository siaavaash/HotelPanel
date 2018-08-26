using Common;
using Constants;
using Data.ViewModel;
using Logic.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelPanel.Controllers
{
    public class AccommodationController : BaseController
    {
        AccommodationBusiness _AccommodationBusiness = new AccommodationBusiness();
        // GET: Accommodation
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            IEnumerable<AccommodationModels.ListNameAccommodation> Result = null;
            return View(Result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult List(AccommodationModels.SearchAccommodation Model)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<AccommodationModels.ListNameAccommodation> Result = _AccommodationBusiness.GetNames(Model);
                return View(Result);
            }
            return View(new List<Data.ViewModel.AccommodationModels.ListNameAccommodation>());
        }
        public ActionResult Images(long AccommodationID)
        {
            var Model = _AccommodationBusiness.GetImages(AccommodationID);
            return View(Model);
        }
        public ActionResult Weather(string AccommodationID)
        {
            long Entry = AccommodationID.IsNormalized() ? long.Parse(AccommodationID) : 0;
            var Model = _AccommodationBusiness.GetWeather(Entry);
            return View(Model);
        }
        [HttpPost]
        public JsonResult Filter(List<long> ImageID)
        {
            bool Result = true;// _AccommodationBusiness.FilterImages(ImageID);
            if (Result)
            {
                iUserStorage.Store(PublicConstants.Session.Message_Success, "Your Accommodation Images Successfully Filtered");
                return Json(Result,JsonRequestBehavior.AllowGet);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Facility(long AccommodationID)
        {
            AccommodationModels.AccommodationFacility Model = new AccommodationModels.AccommodationFacility();
            var Facility = _AccommodationBusiness.GetFacilities(AccommodationID);
            var Description = _AccommodationBusiness.GetDescription(AccommodationID);
            string Name = _AccommodationBusiness.GetAccommodation(AccommodationID).Name;
            Model.Facilities = new List<Data.DataModel.Facility>(Facility);
            Model.Description = Description;
            Model.AccommodationID = AccommodationID;
            Model.Name = Name;
            return View(Model);
        }
    }
}