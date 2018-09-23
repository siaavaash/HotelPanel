using Data.DataModel;
using Service.ServiceModel.IATACodeModels;
using Service.Suppliers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.BusinessObjects
{
    public class IATACodeBusiness
    {
        private bool RemoveExistingData => ConfigurationManager.AppSettings["IataAllowRemove"] == "true" ? true : false;
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
        public void Remove(string code)
        {
            try
            {
                using (var context = new Entities())
                {
                    context.IataAirports.RemoveRange(context.IataAirports.Where(x => x.IataCode == code).ToList());
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void RemoveAll()
        {
            try
            {
                using (var context = new Entities())
                {
                    context.IataAirports.RemoveRange(context.IataAirports.ToList());
                    context.SaveChanges();
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
                GCMAPResponse gcmap = null;
                IATAResponse iata = null;
                Parallel.Invoke(
                    () =>
                    {
                        gcmap = gCMAPCrawler.GetGCMAPData(code);
                    },
                    () =>
                    {
                        iata = IATAScrapper.GetIATALocation(searchBy, code);
                    });
                if (iata.Success && gcmap.Success)
                {
                    foreach (var airport in iata.Data)
                    {
                        if (airport.AirportCode == code.ToUpper())
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
                    if (RemoveExistingData) RemoveAll();
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
                    if (RemoveExistingData) Remove(code);
                    MaptoDbBulk(getData.data);
                    return (true, "Success");
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
