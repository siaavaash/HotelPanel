using Data.DataModel;
using Service.ServiceModel.GIATAModels;
using Service.Suppliers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.BusinessObjects
{
    public class GIATABusiness
    {
        private readonly Context2 context = new Context2();
        private readonly GIATAAccess giataAccess;
        public GIATABusiness()
        {
            giataAccess = new GIATAAccess();
        }

        /// <summary>
        /// Remove All Accommodations in tables
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public void RemoveAll(long from, long to)
        {
            try
            {
                var ids = new ConcurrentBag<long>();
                Parallel.For(from, to + 1, i => ids.Add(i));
                Parallel.Invoke(
                    () =>
                    {
                        using (var context = new Context2())
                        {
                            context.AccommodationTmps.RemoveRange(context.AccommodationTmps.AsParallel().Where(x => ids.Contains(x.AccommodationlID)).ToList());
                            context.SaveChangesAsync().Wait();
                        }
                    },
                    () =>
                    {
                        using (var context = new Context2())
                        {
                            context.AccommodationLocationTmps.RemoveRange(context.AccommodationLocationTmps.AsParallel().Where(x => ids.Contains(x.AccommodationID)).ToList());
                            context.SaveChangesAsync().Wait();
                        }
                    },
                    () =>
                    {
                        using (var context = new Context2())
                        {
                            context.AccomodationSupplierTmps.RemoveRange(context.AccomodationSupplierTmps.AsParallel().Where(x => ids.Contains(x.AccommodationlID)).ToList());
                            context.SaveChangesAsync().Wait();
                        }
                    },
                    () =>
                    {
                        using (var context = new Context2())
                        {
                            context.AccomodationSupplier2Tmp.RemoveRange(context.AccomodationSupplier2Tmp.AsParallel().Where(x => ids.Contains(x.AccommodationlID)).ToList());
                            context.SaveChangesAsync().Wait();
                        }
                    },
                    () =>
                    {
                        using (var context = new Context2())
                        {
                            context.AccommodationAlternativeNames.RemoveRange(context.AccommodationAlternativeNames.AsParallel().Where(x => ids.Contains(x.AccommodationID)).ToList());
                            context.SaveChangesAsync().Wait();
                        }
                    },
                    () =>
                    {
                        using (var context = new Context2())
                        {
                            context.DeActiveAccommodations.RemoveRange(context.DeActiveAccommodations.AsParallel().Where(x => ids.Contains(x.AccommodationlD)).ToList());
                            context.SaveChangesAsync().Wait();
                        }
                    },
                    () =>
                    {
                        using (var context = new Context2())
                        {
                            context.Acc_Airport.RemoveRange(context.Acc_Airport.AsParallel().Where(x => ids.Contains(x.AccommodationlID ?? 0)).ToList());
                            context.SaveChangesAsync().Wait();
                        }
                    }
                    );
                //using (var context = new Context2())
                //{
                //    context.AccommodationTmps.RemoveRange(context.AccommodationTmps.AsParallel().Where(x => ids.Contains(x.AccommodationlID)).ToList());
                //    context.AccommodationLocationTmps.RemoveRange(context.AccommodationLocationTmps.AsParallel().Where(x => ids.Contains(x.AccommodationID)).ToList());
                //    context.AccomodationSupplierTmps.RemoveRange(context.AccomodationSupplierTmps.AsParallel().Where(x => ids.Contains(x.AccommodationlID)).ToList());
                //    context.AccomodationSupplier2Tmp.RemoveRange(context.AccomodationSupplier2Tmp.AsParallel().Where(x => ids.Contains(x.AccommodationlID)).ToList());
                //    context.Acc_Airport.RemoveRange(context.Acc_Airport.AsParallel().Where(x => ids.Contains(x.AccommodationlID ?? 0)).ToList());
                //    context.AccommodationAlternativeNames.RemoveRange(context.AccommodationAlternativeNames.AsParallel().Where(x => ids.Contains(x.AccommodationID)).ToList());
                //    context.DeActiveAccommodations.RemoveRange(context.DeActiveAccommodations.AsParallel().Where(x => ids.Contains(x.AccommodationlD)).ToList());
                //    context.SaveChangesAsync().Wait();
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Map GIATA Data to Db
        /// </summary>
        /// <param name="model">GIATA Response Transfered Model</param>
        /// <param name="message">Error Message</param>
        /// <returns></returns>
        public bool MapGIATADataToDb(GIATADbTransferModel model, out string message)
        {
            try
            {
                var accTmp = new List<AccommodationTmp>();
                var accLocTmp = new List<AccommodationLocationTmp>();
                var altList = new ConcurrentBag<AccommodationAlternativeName>();
                var supplierList = new ConcurrentBag<AccomodationSupplierTmp>();
                var supplier2List = new ConcurrentBag<AccomodationSupplier2Tmp>();
                var airportList = new ConcurrentBag<Acc_Airport>();
                var loc = new List<LocationTmp>();
                Parallel.Invoke(
                    () =>
                    {
                        accTmp.Add(new AccommodationTmp
                        {
                            AccommodationlID = model.AccommodationId,
                            Address = model.Address,
                            AirportCode = model.AirportCode,
                            Axml = model.Xml,
                            ChainID = model.ChainId,
                            CityId = model.CityId,
                            CityName = model.CityName,
                            CountryCode = model.CountryCode,
                            CountryName = model.CountryName,
                            DestinationCode = model.DestinationCode,
                            destinationId = model.DestinationId,
                            Email = model.Email,
                            Fax = model.Fax,
                            lastUpdate = model.LastUpdate,
                            Latitude = model.Latitude,
                            Longitude = model.Longitude,
                            Name = model.Name,
                            Rating = model.Rating,
                            Telephone = model.Telephone,
                            Url = model.Url
                        });
                    },
                    () =>
                    {
                        accLocTmp.Add(new AccommodationLocationTmp
                        {
                            AccommodationID = model.AccommodationId,
                            CityId = model.CityId,
                            CityName = model.CityName,
                            CountryCode = model.CountryCode,
                            LocationID = 0,
                            lastUpdate = model.LastUpdate,
                        });
                    },
                    () =>
                    {
                        loc.Add(new LocationTmp
                        {
                            CityId = model.CityId,
                            CityName = model.CityName,
                            lastUpdate = model.LastUpdate,
                            Name = model.CityName,
                            LocationTypeID = 4,
                            ParentLocationID = model.CountryId + 20000000,
                            CountryCode = model.CountryCode,
                        });
                    },
                    () =>
                    {
                        if (model.Suppliers == null) return;
                        Parallel.ForEach(model.Suppliers, supplier =>
                        {
                            var supplierId = -1;
                            supplier2List.Add(new AccomodationSupplier2Tmp
                            {
                                AccommodationlID = model.AccommodationId,
                                SupplierID = -1,
                                Active = supplier.Active,
                                Code = supplier.Code,
                                ProviderCode = supplier.ProviderCode,
                                ProviderType = supplier.ProviderType,
                                ProviderValue = supplier.ProviderValue,
                                CityId = model.CityId,
                                CityName = model.CityName,
                                CountryName = model.CountryName,
                                lastUpdate = model.LastUpdate,
                                CountryCode = model.CountryCode,
                            });
                            switch (supplier.ProviderCode)
                            {
                                case "metglobal2":
                                    supplierId = 44;
                                    break;
                                case "lowcosthotels":
                                    supplierId = 2;
                                    break;
                                case "lowcostbeds":
                                    supplierId = 2;
                                    break;
                                case "exclusivelyhotels":
                                    supplierId = 3;
                                    break;
                                case "gta_pl0":
                                    supplierId = 9;
                                    break;
                                case "DOTW":
                                    supplierId = 10;
                                    break;
                                case "expedia_ean":
                                    supplierId = 11;
                                    break;
                                case "getabed":
                                    supplierId = 24;
                                    break;
                                case "booking.com":
                                    supplierId = 13;
                                    break;
                                default:
                                    supplierId = -1;
                                    break;
                            }
                            if (supplierId != -1)
                            {
                                supplierList.Add(new AccomodationSupplierTmp
                                {
                                    CountryCode = model.CountryCode,
                                    AccommodationlID = model.AccommodationId,
                                    CityId = model.CityId,
                                    CityName = model.CityName,
                                    CountryName = model.CountryName,
                                    lastUpdate = model.LastUpdate,
                                    ProviderCode = supplier.ProviderCode,
                                    Active = supplier.Active,
                                    ProviderType = supplier.ProviderType,
                                    Code = supplier.Code,
                                    ProviderValue = supplier.ProviderValue,
                                    SupplierID = supplierId,
                                });
                            }
                        });
                    },
                    () =>
                    {
                        if (model.Airports == null) return;
                        Parallel.ForEach(model.Airports, airport =>
                        {
                            airportList.Add(new Acc_Airport
                            {
                                AccommodationlID = model.AccommodationId,
                                CityId = model.CityId,
                                CityName = model.CityName,
                                lastUpdate = model.LastUpdate,
                                CountryCode = model.CountryCode,
                                DestinationCode = model.DestinationCode,
                                destinationId = !string.IsNullOrEmpty(model.DestinationId) ? Convert.ToInt64(model.DestinationId) : 0,
                                rating = model.Rating,
                                category = model.Category,
                                AirPort_Code = airport.iata,
                            });
                        });
                    },
                    () =>
                    {
                        if (model.AlternativeNames == null) return;
                        Parallel.ForEach(model.AlternativeNames, altName =>
                        {
                            altList.Add(new AccommodationAlternativeName
                            {
                                AccommodationID = model.AccommodationId,
                                AlternativeName = altName.Name,
                                EffectiveDate = altName.EffectiveDate,
                                AlternativeNameType = altName.Type,
                            });
                        });
                    });
                using (var context = new Context2())
                {

                    context.AccommodationTmps.AddRange(accTmp);
                    context.AccommodationLocationTmps.AddRange(accLocTmp);
                    context.LocationTmps.AddRange(loc);
                    context.Acc_Airport.AddRange(airportList);
                    context.AccommodationAlternativeNames.AddRange(altList);
                    context.AccomodationSupplierTmps.AddRange(supplierList);
                    context.AccomodationSupplier2Tmp.AddRange(supplier2List);

                    if (context.SaveChangesAsync().Result > 0)
                    {
                        message = null;
                        return true;
                    }
                    else
                    {
                        message = "Map GIATA Data to Database failed.";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message + " -- " + ex.InnerException?.Message;
                return false;
            }
        }
        /// <summary>
        /// Insert Deactive Accommodation
        /// </summary>
        /// <param name="id">Accommodation Id</param>
        /// <param name="movedId">Moved Accommodation Id</param>
        /// <param name="active">Is Active</param>
        /// <returns></returns>
        public bool DeactiveAccommodation(long id, bool? deactive, long movedId = 0)
        {
            try
            {
                using (var context = new Context2())
                {
                    if (deactive.HasValue)
                    {
                        if (deactive.Value)
                        {
                            context.DeActiveAccommodations.Add(new DeActiveAccommodation
                            {
                                AccommodationlD = id,
                                Counter = 0,
                                ISDeactive = true,
                            });
                        }
                        else
                        {
                            context.DeActiveAccommodations.Add(new DeActiveAccommodation
                            {
                                AccommodationlD = id,
                                Counter = 0,
                                ISDeactive = false,
                            });
                        }
                    }
                    if (movedId != 0)
                    {
                        context.DeActiveAccommodations.Add(new DeActiveAccommodation
                        {
                            AccommodationlD = id,
                            MovedToAccommodationlD = movedId,
                            ISDeactive = true,
                        });
                    }
                    return context.SaveChanges() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Map GIATA Data to Model
        /// </summary>
        /// <param name="sourceModel"></param>
        /// <param name="resultModel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool MapGIATADataToModel(properties sourceModel, out GIATADbTransferModel resultModel, out string message)
        {
            try
            {
                var address = sourceModel.property.addresses?.address;
                resultModel = new GIATADbTransferModel
                {
                    AccommodationId = Convert.ToInt64(sourceModel.property.giataId),
                    Address = address?.addressLine[0]?.Value + " " + address?.addressLine[1]?.Value,
                    AirportCode = sourceModel.property.airports?[0].iata,
                    Category = sourceModel.property.category.ToString(),
                    ChainId = Convert.ToInt64(sourceModel.property.chains?.chain?.chainId),
                    CityId = Convert.ToInt64(sourceModel.property.city?.cityId),
                    CityName = sourceModel.property.city?.Value,
                    CountryCode = sourceModel.property.country,
                    CountryId = 0,
                    CountryName = sourceModel.property.country,
                    Email = sourceModel.property.emails?.email,
                    Fax = sourceModel.property.phones?.FirstOrDefault(x => x.tech == "fax")?.Value.ToString(),
                    Latitude = sourceModel.property.geoCodes?.geoCode?.latitude.ToString(),
                    Longitude = sourceModel.property.geoCodes?.geoCode?.longitude.ToString(),
                    LastUpdate = sourceModel.property.lastUpdate ?? DateTime.Now,
                    Name = sourceModel.property.name,
                    Rating = sourceModel.property.ratings?.rating?.value.ToString(),
                    Url = sourceModel.property.urls?.url,
                    Telephone = sourceModel.property.phones?.FirstOrDefault(x => x.tech == "voice")?.Value.ToString(),
                    DestinationId = sourceModel.property.destination?.destinationId.ToString(),
                    DestinationCode = sourceModel.property.destination?.Value,
                    Suppliers = new List<AccommodationSupplier>(),
                    AlternativeNames = sourceModel.property.alternativeNames?.ToList().Select(x => new AlternativeName
                    {
                        EffectiveDate = x.effectiveDate,
                        Name = x.Value,
                        Type = x.alternativeNameType,
                    }).ToList(),
                    Airports = sourceModel.property.airports?.ToList().Select(x => new propertiesPropertyAirport
                    {
                        iata = x.iata,
                    }).ToList(),
                };
                foreach (var provider in sourceModel.property.propertyCodes)
                    foreach (var code in provider.code)
                        resultModel.Suppliers.Add(new AccommodationSupplier
                        {
                            Active = code.status == null ? false : true,
                            Code = code.value.First().Value,
                            ProviderCode = provider.providerCode,
                            ProviderType = provider.providerType,
                            ProviderValue = code.value.First().Value,
                        });
                message = null;
                return true;
            }
            catch (Exception ex)
            {
                resultModel = null;
                message = ex.Message;
                return false;
            }
        }

        public (bool serviceSuccess, bool dbSuccess, string message) Map(long id)
        {
            string message = null;
            try
            {
                var serviceResult = giataAccess.GetPropertiesById(id);
                if (serviceResult.Success)
                {
                    if (serviceResult.Model is properties)
                    {
                        if (MapGIATADataToModel((properties)serviceResult.Model, out GIATADbTransferModel mapDb, out message))
                        {
                            if (MapGIATADataToDb(mapDb, out message))
                                return (true, true, message);
                        }
                        return (true, false, message);
                    }
                    else
                    {
                        var str = ((error)serviceResult.Model).description.href;
                        var movedId = Convert.ToInt64(str.Substring(str.LastIndexOf('/') + 1));
                        message = ((error)serviceResult.Model).description.Value;
                        if (DeactiveAccommodation(id, null, movedId: movedId))
                            return (true, true, message);
                        return (true, false, message);
                    }
                }
                message = serviceResult.Error.Text;
                return (false, false, message);
            }
            catch (Exception ex)
            {
                return (true, false, ex.Message);
            }
        }

        /// <summary>
        /// Map Range Data from GIATA Service to Db
        /// </summary>
        /// <param name="from">from giata id</param>
        /// <param name="to">to giata id</param>
        /// <returns>giata ids that failed</returns>
        public List<MapResult> MapRange(long from, long to)
        {
            if (from > to)
                throw new ArgumentException("Invalid parameter(s).");
            RemoveAll(from, to);
            var returnList = new List<MapResult>();
            Parallel.For(from, to + 1, i =>
            {
                var mapRs = Map(i);
                if (!mapRs.serviceSuccess)
                {
                    var tryMapRs = TryMap(i);
                    if (!tryMapRs.Item1)
                    {
                        returnList.Add(new MapResult
                        {
                            Id = i,
                            Message = tryMapRs.Item2,
                            MapToDbSuccess = false,
                            ServiceSuccess = false,
                        });
                        return;
                    }
                    returnList.Add(new MapResult
                    {
                        Id = i,
                        Message = tryMapRs.Item2,
                        MapToDbSuccess = true,
                        ServiceSuccess = true,
                    });
                    return;
                }
                if (mapRs.dbSuccess)
                {
                    returnList.Add(new MapResult
                    {
                        Id = i,
                        Message = mapRs.message,
                        ServiceSuccess = true,
                        MapToDbSuccess = true,
                    });
                    return;
                }
                DeactiveAccommodation(i, deactive: true);
                returnList.Add(new MapResult
                {
                    Id = i,
                    Message = mapRs.message,
                    ServiceSuccess = true,
                    MapToDbSuccess = false,
                });
            });
            return returnList;
        }
        /// <summary>
        /// Try Map 3 attempts
        /// </summary>
        /// <param name="id"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public (bool, string) TryMap(long id)
        {
            string message = null;
            try
            {
                for (int i = 0; i < 2; i++)
                {
                    var mapRs = Map(id);
                    message = mapRs.message;
                    if (mapRs.serviceSuccess && mapRs.dbSuccess)
                        return (true, null);
                }
                DeactiveAccommodation(id, deactive: true);
                return (false, message ?? "Failed.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
