using Data.DataModel;
using Service.ServiceModel.IATACodeModels;
using Service.Suppliers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.BusinessObjects
{
    public class IATACodeBusiness
    {
        private GCMAPCrawler gCMAPCrawler;
        public IATACodeBusiness()
        {
            gCMAPCrawler = new GCMAPCrawler();
        }
        public void MapToDb(IataAirport model)
        {
            try
            {
                using (var context = new Entities())
                {
                    context.IataAirports.Add(model);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void MaptoDbBulk(List<IataAirport> model)
        {
            try
            {
                using (var context = new Entities())
                {
                    context.IataAirports.AddRange(model);
                    context.SaveChangesAsync().Wait();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public (bool success, List<IataAirport> data) GetIATAData(Common.IATASearchBy searchBy, string code)
        {
            try
            {
                var result = new List<IataAirport>();
                var gcmap = gCMAPCrawler.GetGCMAPData(code);
                var iata = IATAScrapper.GetIATALocation(searchBy, code);
                if (iata.Success && gcmap.Success)
                {
                    foreach (var airport in iata.Data)
                    {
                        if (airport.AirportCode == code)
                        {
                            result.Add(new IataAirport
                            {
                                CityName = airport.CityName,
                                CountryName = gcmap.Data.Country,
                                IataCode = airport.AirportCode,
                                Latitude = gcmap.Data.Latitude,
                                Longitude = gcmap.Data.Longitude,
                                Name = airport.AirportName,
                                Type = gcmap.Data.Type
                            });
                        }
                    }
                    return (true, result);
                }
                else
                    return (false, null);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public (bool success, List<IataAirport> data) GetIATAData()
        {
            try
            {
                var result = new List<IataAirport>();
                var iataCodeResult = IataCodeAccess.GetIataCodes();
                if (iataCodeResult.Success)
                {
                    Parallel.ForEach(iataCodeResult.Data, item =>
                    {
                        var gcmap = gCMAPCrawler.GetGCMAPData(item.Code);
                        var iata = IATAScrapper.GetIATALocation(Common.IATASearchBy.ByLocationCode, item.Code);
                        if (iata.Success && gcmap.Success)
                        {
                            foreach (var airport in iata.Data)
                            {
                                if (airport.AirportCode == item.Code)
                                {
                                    result.Add(new IataAirport
                                    {
                                        CityName = airport.CityName,
                                        CountryName = gcmap.Data.Country,
                                        IataCode = airport.AirportCode,
                                        Latitude = gcmap.Data.Latitude,
                                        Longitude = gcmap.Data.Longitude,
                                        Name = airport.AirportName,
                                        Type = gcmap.Data.Type
                                    });
                                }
                            }
                        }
                        else
                            result.Add(new IataAirport
                            {
                                IataCode = item.Code,
                                Name = item.Name,
                            });
                    });
                    return (true, result);
                }
                return (false, null);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public (bool success, string message) MapIata()
        {
            try
            {
                var getData = GetIATAData();
                if (getData.success)
                {
                    MaptoDbBulk(getData.data);
                }
                return (false, "Get Data failed.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public (bool success, string message) MapIata(string code, Common.IATASearchBy searchBy = Common.IATASearchBy.ByLocationCode)
        {
            try
            {
                var getData = GetIATAData(searchBy, code);
                if (getData.success)
                {
                    MaptoDbBulk(getData.data);
                }
                return (false, "Get Data failed.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
