using Logic.BusinessObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HotelPanel.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    public class GIATAController : Controller
    {
        private GIATABusiness giataBusiness = new GIATABusiness();

        // GET: GIATA
        public ActionResult Index()
        {
            return View();
        }

        // Map GIATA Data to DB
        public ActionResult MapByID(long from, long to)
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

        public async Task<FileResult> DownloadPropertiesByIDAsync(byte part, byte version)
        {
            try
            {
                var result = await giataBusiness.DownloadPropertiesByIDAsync((Service.ServiceModel.GIATAModels.Version)version, part);
                return File(result, "application/zip", $"HotelsByID-Part{part} ({Enum.GetName(typeof(Service.ServiceModel.GIATAModels.Version), version)}).zip");
            }
            catch (Exception)
            {

                throw;
            }
        }

        // Get Properties by ID
        [Route("giata/1.{version:regex(0|1)}/properties/{parameter}/{filter?}")]
        public ContentResult Properties(byte version, string parameter, string filter = "")
        {
            try
            {
                return Content("hiiiiiiii.");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<FileResult> GetHotelsByProviders(string version, byte provider)
        {
            var content = await giataBusiness.GetHotelsByProviderAsync(version, (Service.ServiceModel.GIATAModels.Filter)provider);
            return File(content, "application/zip", $"{(Service.ServiceModel.GIATAModels.Filter)provider}_{version}");
        }
        public async Task<FileResult> GetHotelsByCities(string version, byte part)
        {
            var content = await giataBusiness.GetHotelsByCitiesAsync(version, part);
            return File(content, "application/zip", $"GetHotelsByCities_{version}_Part{part}");
        }
        public async Task<FileResult> GetHotelsByCountries(string version)
        {
            var content = await giataBusiness.GetHotelsByCountriesAsync(version);
            return File(content, "application/zip", $"GetHotelsByCountries_{version}");
        }
    }
}