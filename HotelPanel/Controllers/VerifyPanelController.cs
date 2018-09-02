using Data.ViewModel.VerifyPanelViewModels;
using Logic.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelPanel.Controllers
{
    public class VerifyPanelController : Controller
    {
        private readonly VerifyPanelBusiness verifyPanelBusiness = new VerifyPanelBusiness();
        // GET: VerifyPanel
        public ActionResult Index() => View();

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
        public JsonResult Verify(HotelInfoViewModel model)
        {
            try
            {
                if (verifyPanelBusiness.Verify(model, out string message))
                    return Json(new { success = true, data = message }, JsonRequestBehavior.AllowGet);
                return Json(new { success = false, data = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}