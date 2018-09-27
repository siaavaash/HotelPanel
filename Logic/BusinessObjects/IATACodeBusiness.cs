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
        public (bool success, string message) MaptoDbBulk(List<IataAirport> model)
        {
            try
            {
                using (var context = new Entities())
                {
                    context.IataAirports.AddRange(model);
                    var result = context.SaveChangesAsync().Result > 0 ? true : false;
                    if (result)
                        return (result, "");
                    return (false, "Map to Db failed.");
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
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
                if (iata.Success || gcmap.Success)
                {
                    if (iata?.Data?.Count > 0)
                    {
                        foreach (var airport in iata.Data)
                        {
                            if (airport.AirportCode == code.ToUpper())
                            {
                                result.Add(new IataAirport
                                {
                                    CityName = airport.CityName,
                                    CountryName = gcmap.Data?.Country,
                                    IataCode = airport.AirportCode,
                                    Latitude = gcmap.Data?.Latitude,
                                    Longitude = gcmap.Data?.Longitude,
                                    Name = airport.AirportName,
                                    Type = gcmap.Data?.Type,
                                    Nearby = gcmap.Data?.Nearby,
                                    OldName = gcmap.Data?.OldName,
                                    TimeZone = gcmap.Data?.TimeZone
                                });
                            }
                        }
                    }
                    else
                    {
                        result.Add(new IataAirport
                        {
                            CityName = gcmap.Data?.City,
                            CountryName = gcmap.Data?.Country,
                            IataCode = gcmap.Data?.IATACode,
                            Latitude = gcmap.Data?.Latitude,
                            Longitude = gcmap.Data?.Longitude,
                            Name = gcmap.Data?.Name,
                            Type = gcmap.Data?.Type,
                            Nearby = gcmap.Data?.Nearby,
                            OldName = gcmap.Data?.OldName,
                            TimeZone = gcmap.Data?.TimeZone
                        });
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
        public (bool success, string message, List<IATACodeViewModel> data) GetAndMapIATAData()
        {
            try
            {
                var result = new ConcurrentBag<IATACodeViewModel>();
                var iataCodeResult = IataCodeAccess.GetIataCodes();
                if (iataCodeResult.Success)
                {
                    Parallel.ForEach(iataCodeResult.Data, /*new ParallelOptions { MaxDegreeOfParallelism = 10 },*/ item =>
                      {
                          var getData = GetIATAData(Common.IATASearchBy.ByLocationCode, item.Code);
                          if (getData.success)
                          {
                              if (RemoveExistingData) Remove(item.Code);
                              var (success, message) = MaptoDbBulk(getData.data);
                              if (success)
                              {
                                  result.Add(new IATACodeViewModel
                                  {
                                      Code = item.Code,
                                      GetSuccess = true,
                                      MapSuccess = true,
                                      Message = message
                                  });
                              }
                              else
                                  result.Add(new IATACodeViewModel
                                  {
                                      Code = item.Code,
                                      GetSuccess = true,
                                      MapSuccess = false,
                                      Message = message
                                  });
                          }
                          else
                              result.Add(new IATACodeViewModel
                              {
                                  Code = item.Code,
                                  GetSuccess = false,
                                  MapSuccess = false,
                              });
                      });
                    return (true, "", result.ToList());
                }
                return (false, "Get iata codes failed.", null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }
        public (bool success, string message, IATACodeViewModel data) MapIata(string code, Common.IATASearchBy searchBy = Common.IATASearchBy.ByLocationCode)
        {
            try
            {
                var getData = GetIATAData(searchBy, code);
                if (getData.success)
                {
                    if (RemoveExistingData) Remove(code);
                    var map = MaptoDbBulk(getData.data);
                    if (map.success)
                    {
                        return (true, "", new IATACodeViewModel
                        {
                            Code = code,
                            GetSuccess = true,
                            MapSuccess = true,
                            Message = map.message
                        });
                    }
                    return (true, "", new IATACodeViewModel
                    {
                        Code = code,
                        GetSuccess = true,
                        MapSuccess = false,
                        Message = map.message
                    });
                }
                return (true, "", new IATACodeViewModel
                {
                    Code = code,
                    GetSuccess = false,
                    MapSuccess = false,
                    Message = "Get Iata Data failed."
                });

            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }
    }
}
