using Logic.BusinessObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HotelPanel.Controllers
{
    public class DotwController : Controller
    {
        private DOTWBusiness dotwBusiness = new DOTWBusiness();

        // GET: DOTW Hotels Info
        public async Task<FileResult> DownloadAllDotwHotels(byte part)
        {
            try
            {
                return File(await dotwBusiness.GetHotelsByAllCitiesAsync(part), "application/zip", $"AllHotelsByCity_Part{part}.zip");
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Download Cities By Country Json
        public async Task<FileResult> DownloadCitiesByCountry()
        {
            var json = JsonConvert.SerializeObject(await dotwBusiness.GetCitiesByCountryAsync());
            return File(Encoding.UTF8.GetBytes(json), "application/json", "CitiesByCountry.json");
        }

        // GET: Dotw
        public ActionResult Index()
        {
            return View();
        }
    }
}