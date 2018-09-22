using Logic.BusinessObjects;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HotelPanel.Controllers
{
    public class MappingController : BaseController
    {
        private readonly GIATABusiness giataBusiness;
        private readonly GeocodingBusiness geocodingBusiness;
        private readonly IATACodeBusiness iATACodeBusiness;
        public MappingController()
        {
            giataBusiness = new GIATABusiness();
            geocodingBusiness = new GeocodingBusiness();
            iATACodeBusiness = new IATACodeBusiness();
        }

        // GET: GIATA
        public ActionResult GIATA() => View();

        // GET: Map GIATA Data
        public ActionResult MapGIATAData(long from, long to)
        {
            try

            {
                var allData = giataBusiness.MapRange(from, to).OrderBy(x => x.Id);
                var size = Convert.ToInt32(Math.Ceiling(allData.Count() / 5d));
                var status = JsonConvert.SerializeObject(new { success = true });
                var json = new string[5];
                for (int i = 0; i < 5; i++)
                {
                    var data = allData.Skip(i * size).Take(size).ToList();
                    if (data.Count() > 0)
                        json[i] = JsonConvert.SerializeObject(data).Replace("[", i > 0 ? "," : "").Replace("]", "");
                }
                var result = status.Remove(status.LastIndexOf("}")) + ",\"data\":[" + json[0] + json[1] + json[2] + json[3] + json[4] + "]}";
                return Content(result, "application/json");
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"{ex.Message} -- {ex.InnerException?.Message}" }, JsonRequestBehavior.AllowGet);
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

        // GET: Map Iata Codes
        public JsonResult MapIataCode(string code, byte searchBy)
        {
            try
            {
                var result = iATACodeBusiness.MapIata(code, (Common.IATASearchBy)searchBy);
                return Json(new { success = result.success, message = result.message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult MapAllIataCodes()
        {
            try
            {
                var result = iATACodeBusiness.MapIata();
                return Json(new { success = result.success, message = result.message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}