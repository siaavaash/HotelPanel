using Logic.BusinessObjects;
using System;
using System.Web.Mvc;

namespace HotelPanel.Controllers
{
    public class GIATAController : BaseController
    {
        private readonly GIATABusiness giataBusiness;
        public GIATAController()
        {
            giataBusiness = new GIATABusiness();
        }

        // GET: GIATA
        public ActionResult Index() => View();

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
    }
}