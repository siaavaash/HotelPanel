using Logic.BusinessObjects;
using System;
using System.Web.Mvc;

namespace HotelPanel.Controllers
{
    public class MappingController : BaseController
    {
        private readonly GIATABusiness giataBusiness;
        private readonly GeocodingBusiness geocodingBusiness;
        public MappingController()
        {
            giataBusiness = new GIATABusiness();
            geocodingBusiness = new GeocodingBusiness();
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

        // GET: Get Geocodes All
        public JsonResult GetAllGeocodes()
        {
            try
            {
                return Json(new { success = false, data = geocodingBusiness.MapGeocodeAll() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Get Geocodes Range
        public JsonResult GetGeocodes(long from, long to)
        {
            try
            {
                return Json(new { success = false, data = geocodingBusiness.MapGeocodeRange(from, to) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}