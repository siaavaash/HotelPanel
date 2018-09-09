using Data.ViewModel.VerifyPanelViewModels;
using Logic.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelPanel.Controllers
{
    public class VerifyPanelController : BaseController
    {
        private readonly VerifyPanelBusiness verifyPanelBusiness = new VerifyPanelBusiness();
        // GET: VerifyPanel
        public ActionResult Details(long? id)
        {
            try
            {
                if (id != null)
                    return View(verifyPanelBusiness.GetAccommodationInfo(id.Value));
                throw new Exception();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JsonResult GetAccommodationInfo(long id)
        {
            try
            {
                return Json(new { success = true, data = verifyPanelBusiness.GetAccommodationInfo(id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Verify(HotelInfoViewModel model, List<long> addedFacilities, List<long> removedFacilities)
        {
            try
            {
                if (verifyPanelBusiness.Verify(model, addedFacilities, removedFacilities, out string message))
                    return Json(new { success = true, data = message }, JsonRequestBehavior.AllowGet);
                return Json(new { success = false, data = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetFacilitiesList(long id)
        {
            try
            {
                return Json(verifyPanelBusiness.GetFacilities(id), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}