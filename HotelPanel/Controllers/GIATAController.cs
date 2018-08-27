using Logic.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelPanel.Controllers
{
    public class GIATAController : Controller
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
                return Json(giataBusiness.MapRange(from, to));
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message });
            }
        }
    }
}