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
                var size = allData.Count() / 5;
                string status = "", partOneJson = "", partTwoJson = "", partThreeJson = "", partFourJson = "", partFiveJson = "";
                Parallel.Invoke(
                            () =>
                            {
                                status = JsonConvert.SerializeObject(new { success = true });
                            },
                            () =>
                            {
                                partOneJson = JsonConvert.SerializeObject(allData.Skip(0 * size).Take(size));
                            },
                            () =>
                            {
                                partTwoJson = JsonConvert.SerializeObject(allData.Skip(1 * size).Take(size));
                            },
                            () =>
                            {
                                partThreeJson = JsonConvert.SerializeObject(allData.Skip(2 * size).Take(size));
                            },
                            () =>
                            {
                                partFourJson = JsonConvert.SerializeObject(allData.Skip(3 * size).Take(size));
                            },
                            () =>
                            {
                                partFiveJson = JsonConvert.SerializeObject(allData.Skip(4 * size));
                            });





                var result = status.Remove(status.LastIndexOf("}")) + ",\"data\":" + partOneJson.Replace("]", ",")
                                                                                        + partTwoJson.Replace("]", ",").Remove(partTwoJson.IndexOf("["), 1)
                                                                                        + partThreeJson.Replace("]", ",").Remove(partThreeJson.IndexOf("["), 1)
                                                                                        + partFourJson.Replace("]", ",").Remove(partFourJson.IndexOf("["), 1)
                                                                                        + partFiveJson.Remove(partFiveJson.IndexOf("["), 1)
                                                                                        + "}";
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