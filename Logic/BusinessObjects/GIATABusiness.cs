using Data.DataModel;
using Ionic.Zip;
using Service.ServiceModel.GIATAModels;
using Service.Suppliers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.BusinessObjects
{
    public class GIATABusiness
    {
        private readonly Context2 context = new Context2();
        private readonly GIATAAccess giataAccess = new GIATAAccess();

        private bool TruncateTables => ConfigurationManager.AppSettings["TruncateDb"] == "true" ? true : false;

        /// <summary>
        /// Remove All Relative Data in DB
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public void RemoveAll(long from, long to)
        {
            try
            {
                Parallel.Invoke(
                    () =>
                    {
                        using (var context = new Context2())
                        {
                            context.BulkDelete(context.AccommodationTmps.AsParallel().Where(x => x.AccommodationlID <= to && x.AccommodationlID >= from).ToList());
                        }
                    },
                    () =>
                    {
                        using (var context = new Context2())
                        {
                            context.BulkDelete(context.AccommodationLocationTmps.AsParallel().Where(x => x.AccommodationID <= to && x.AccommodationID >= from).ToList());
                        }
                    },
                    () =>
                    {
                        using (var context = new Context2())
                        {
                            context.BulkDelete(context.AccomodationSupplierTmps.AsParallel().Where(x => x.AccommodationlID <= to && x.AccommodationlID >= from).ToList());
                        }
                    },
                    () =>
                    {
                        using (var context = new Context2())
                        {
                            context.BulkDelete(context.AccomodationSupplier2Tmp.AsParallel().Where(x => x.AccommodationlID <= to && x.AccommodationlID >= from).ToList());
                        }
                    },
                    () =>
                    {
                        using (var context = new Context2())
                        {
                            context.BulkDelete(context.AccommodationAlternativeNames.AsParallel().Where(x => x.AccommodationID <= to && x.AccommodationID >= from).ToList());
                        }
                    },
                    () =>
                    {
                        using (var context = new Context2())
                        {
                            context.BulkDelete(context.DeActiveAccommodations.AsParallel().Where(x => x.AccommodationlD <= to && x.AccommodationlD >= from).ToList());
                        }
                    },
                    () =>
                    {
                        using (var context = new Context2())
                        {
                            context.BulkDelete(context.Acc_Airport.AsParallel().Where(x => x.AccommodationlID <= to && x.AccommodationlID >= from).ToList());
                        }
                    }
                    );
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
                var accTmp = new ConcurrentBag<AccommodationTmp>();
                var accLocTmp = new ConcurrentBag<AccommodationLocationTmp>();
                var altList = new ConcurrentBag<AccommodationAlternativeName>();
                var supplierList = new ConcurrentBag<AccomodationSupplierTmp>();
                var supplier2List = new ConcurrentBag<AccomodationSupplier2Tmp>();
                var airportList = new ConcurrentBag<Acc_Airport>();
                var loc = new ConcurrentBag<LocationTmp>();
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
                            ParentLocationID = model.CountryId ?? 0 + 20000000,
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
                                AirPort_Code = airport.Iata,
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
                    context.BulkInsert(accTmp);
                    context.BulkInsert(accLocTmp);
                    context.BulkInsert(loc);
                    context.BulkInsert(airportList);
                    context.BulkInsert(altList);
                    context.BulkInsert(supplierList);
                    context.BulkInsert(supplier2List);

                    message = null;
                    return true;

                    //context.Configuration.AutoDetectChangesEnabled = false;

                    //context.AccommodationTmps.AddRange(accTmp);
                    //context.AccommodationLocationTmps.AddRange(accLocTmp);
                    //context.LocationTmps.AddRange(loc);
                    //context.Acc_Airport.AddRange(airportList);
                    //context.AccommodationAlternativeNames.AddRange(altList);
                    //context.AccomodationSupplierTmps.AddRange(supplierList);
                    //context.AccomodationSupplier2Tmp.AddRange(supplier2List);

                    //context.ChangeTracker.DetectChanges();

                    //if (context.SaveChanges() > 0)
                    //{
                    //    message = null;
                    //    return true;
                    //}
                    //else
                    //{
                    //    message = "Map GIATA Data to Database failed.";
                    //    return false;
                    //}
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
                    return context.SaveChangesAsync().Result > 0 ? true : false;
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
        public (bool success, GIATADbTransferModel resultModel) MapGIATADataToModel(Properties sourceModel, out string message)
        {
            var resultModel = new GIATADbTransferModel();
            try
            {
                var address = sourceModel.Property.Addresses?.Address;
                resultModel = new GIATADbTransferModel
                {
                    AccommodationId = long.Parse(sourceModel.Property.GiataId),
                    Address = address?.AddressLine[0]?.Text + " " + address?.AddressLine[1]?.Text,
                    AirportCode = sourceModel.Property.Airports?.Airport[0]?.Iata,
                    Category = sourceModel.Property.Category,
                    ChainId = string.IsNullOrEmpty(sourceModel.Property.Chains?.Chain?.ChainId) ? (long?)null : long.Parse(sourceModel.Property.Chains?.Chain?.ChainId),
                    CityId = string.IsNullOrEmpty(sourceModel.Property.City?.CityId) ? (long?)null : long.Parse(sourceModel.Property.City?.CityId),
                    CityName = sourceModel.Property.City?.Text,
                    CountryCode = sourceModel.Property.Country,
                    CountryId = 0,
                    CountryName = sourceModel.Property.Country,
                    Email = sourceModel.Property.Emails?.Email,
                    Fax = sourceModel.Property.Phones?.Phone.FirstOrDefault(x => x.Tech == "fax")?.Text,
                    Latitude = sourceModel.Property.GeoCodes?.GeoCode?.Latitude,
                    Longitude = sourceModel.Property.GeoCodes?.GeoCode?.Longitude,
                    LastUpdate = string.IsNullOrEmpty(sourceModel.Property.LastUpdate) ? (DateTime?)null : DateTime.Parse(sourceModel.Property.LastUpdate),
                    Name = sourceModel.Property.Name,
                    Rating = sourceModel.Property.Ratings?.Rating?.Value,
                    Url = sourceModel.Property.Urls?.Url,
                    Telephone = sourceModel.Property.Phones?.Phone?.FirstOrDefault(x => x.Tech == "voice")?.Text,
                    DestinationId = sourceModel.Property.Destination?.DestinationId,
                    DestinationCode = sourceModel.Property.Destination?.Text,
                    Suppliers = new ConcurrentBag<AccommodationSupplier>(),
                    AlternativeNames = sourceModel.Property.AlternativeNames?.AlternativeName?.Select(x => new AlternativeNameViewModel
                    {
                        EffectiveDate = string.IsNullOrEmpty(x.EffectiveDate) ? (DateTime?)null : DateTime.Parse(x.EffectiveDate),
                        Name = x.Text,
                        Type = x.AlternativeNameType,
                    }).ToList(),
                    Airports = sourceModel.Property.Airports?.Airport?.ToList(),
                };
                Parallel.ForEach(sourceModel.Property.PropertyCodes?.Provider ?? new List<Provider>(), provider =>
                {
                    foreach (var code in provider.Code)
                        resultModel.Suppliers.Add(new AccommodationSupplier
                        {
                            Active = code.Status == "inactive" ? false : true,
                            Code = code.Value.Count > 1 ? code.Value?.FirstOrDefault(x => x.Name == "City Code")?.Text + "|" + code.Value?.FirstOrDefault(x => x.Name == "Hotel Code")?.Text : code.Value[0]?.Text,
                            ProviderCode = provider.ProviderCode,
                            ProviderType = provider.ProviderType,
                            ProviderValue = code.Value.Count > 1 ? code.Value?.FirstOrDefault(x => x.Name == "City Code")?.Text + "|" + code.Value?.FirstOrDefault(x => x.Name == "Hotel Code")?.Text : code.Value[0]?.Text,
                        });
                });
                message = null;
                return (true, resultModel);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return (false, null);
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
                    if (serviceResult.Model is Properties)
                    {
                        var getData = MapGIATADataToModel((Properties)serviceResult.Model, out message);
                        if (getData.success)
                        {
                            if (MapGIATADataToDb(getData.resultModel, out message))
                                return (true, true, message);
                        }
                        return (true, false, message);
                    }
                    else if (serviceResult.Model is Error)
                    {
                        var str = ((Error)serviceResult.Model).Description.Href;
                        var movedId = Convert.ToInt64(str.Substring(str.LastIndexOf('/') + 1));
                        message = ((Error)serviceResult.Model).Description.Text;
                        if (DeactiveAccommodation(id, null, movedId: movedId))
                            return (true, true, message);
                        return (true, false, message);
                    }
                    message = serviceResult.Error.Text;
                    return (true, false, message);
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
        public ConcurrentBag<MapResult> MapRange(long from, long to)
        {
            try
            {
                if (from > to)
                    throw new ArgumentException("Invalid parameter(s).");
                if (TruncateTables) RemoveAll(from, to);
                var returnList = new ConcurrentBag<MapResult>();
                var actions = new ConcurrentBag<Action>();
                Parallel.For(from, to + 1, i =>
                {
                    actions.Add(() =>
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
                });
                Parallel.Invoke(actions.ToArray());


                //Parallel.For(from, to + 1, i =>
                // {
                //     var mapRs = Map(i);
                //     if (!mapRs.serviceSuccess)
                //     {
                //         var tryMapRs = TryMap(i);
                //         if (!tryMapRs.Item1)
                //         {
                //             returnList.Add(new MapResult
                //             {
                //                 Id = i,
                //                 Message = tryMapRs.Item2,
                //                 MapToDbSuccess = false,
                //                 ServiceSuccess = false,
                //             });
                //             return;
                //         }
                //         returnList.Add(new MapResult
                //         {
                //             Id = i,
                //             Message = tryMapRs.Item2,
                //             MapToDbSuccess = true,
                //             ServiceSuccess = true,
                //         });
                //         return;
                //     }
                //     if (mapRs.dbSuccess)
                //     {
                //         returnList.Add(new MapResult
                //         {
                //             Id = i,
                //             Message = mapRs.message,
                //             ServiceSuccess = true,
                //             MapToDbSuccess = true,
                //         });
                //         return;
                //     }
                //     DeactiveAccommodation(i, deactive: true);
                //     returnList.Add(new MapResult
                //     {
                //         Id = i,
                //         Message = mapRs.message,
                //         ServiceSuccess = true,
                //         MapToDbSuccess = false,
                //     });
                // });
                return returnList;
            }
            catch (Exception ex)
            {
                throw new Exception($"Business Layer Error: {ex.Message} -- {ex.InnerException?.Message}");
            }
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

        /// <summary>
        /// Download Properties by ID in xml
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public byte[] DownloadPropertiesByID(long from, long to, Service.ServiceModel.GIATAModels.Version version)
        {
            try
            {
                var output = new MemoryStream();
                using (var zip = new ZipFile())
                {
                    //zip.MaxOutputSegmentSize = 50 * 1024 * 1024;
                    Parallel.For(from, to + 1, i =>
                    {
                        var ver = version == Service.ServiceModel.GIATAModels.Version.latest ? "1.latest" : "1.0";
                        var url = GIATAAccess.GetUrlByParameters(ver, Method.properties, i.ToString());
                        var file = giataAccess.GetFileByUrl(i.ToString(), ".xml", url);
                        zip.AddEntry($"{file.Name}{file.Extention}", file.Contents);
                    });
                    zip.Save(output);
                }
                return output.ToArray();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
