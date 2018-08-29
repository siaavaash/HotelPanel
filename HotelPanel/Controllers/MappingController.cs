using Logic.BusinessObjects;
using System;
using System.Web.Mvc;

namespace HotelPanel.Controllers
{
    public class MappingController : BaseController
    {
        private readonly GIATABusiness giataBusiness;
        public MappingController()
        {
            giataBusiness = new GIATABusiness();
        }

        // GET: GIATA
        public ActionResult GIATA() => View();

        // GET: Map GIATA Data
        public JsonResult MapGIATAData(long from, long to)
        {
            try
            {
                return Json(new { success = true, data = giataBusiness.MapRange(from, to) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Geocode
        public ActionResult Geocode() => View();

        // GET: Get Geocodes
        public JsonResult GetGeocodes(long from, long to)
        {
            try
            {

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}