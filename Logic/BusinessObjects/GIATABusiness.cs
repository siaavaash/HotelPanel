using Data.MapDataModel;
using Service.ServiceModel.GIATAModels;
using Service.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.BusinessObjects
{
    public class GIATABusiness
    {
        private readonly MapDbContext context;
        private readonly GIATAAccess giataAccess;
        public GIATABusiness()
        {
            context = new MapDbContext();
            giataAccess = new GIATAAccess();
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
                context.AccommodationTmps.RemoveRange(context.AccommodationTmps.Where(x => x.AccommodationlID == model.AccommodationId).ToList());
                context.AccommodationTmps.Add(new AccommodationTmp
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
                context.AccommodationLocationTmps.RemoveRange(context.AccommodationLocationTmps.Where(x => x.AccommodationID == model.AccommodationId).ToList());
                context.AccommodationLocationTmps.Add(new AccommodationLocationTmp
                {
                    AccommodationID = model.AccommodationId,
                    CityId = model.CityId,
                    CityName = model.CityName,
                    CountryCode = model.CountryCode,
                    LocationID = 0,
                    lastUpdate = model.LastUpdate,
                });
                context.LocationTmps.Add(new LocationTmp
                {
                    CityId = model.CityId,
                    CityName = model.CityName,
                    lastUpdate = model.LastUpdate,
                    Name = model.CityName,
                    LocationTypeID = 4,
                    ParentLocationID = model.CountryId + 20000000,
                    CountryCode = model.CountryCode,
                });
                Parallel.ForEach(model.Suppliers, supplier =>
                 {
                     var supplierId = -1;
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
                         context.AccomodationSupplierTmps.RemoveRange(context.AccomodationSupplierTmps.Where(x => x.AccommodationlID == model.AccommodationId).ToList());
                         context.AccomodationSupplierTmps.Add(new AccomodationSupplierTmp
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
                Parallel.ForEach(model.Suppliers, (supplier) =>
                 {
                     context.AccomodationSupplier2Tmp.RemoveRange(context.AccomodationSupplier2Tmp.Where(x => x.AccommodationlID == model.AccommodationId).ToList());
                     context.AccomodationSupplier2Tmp.Add(new AccomodationSupplier2Tmp
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
                 });
                Parallel.ForEach(model.Airports, (airport) =>
                 {
                     context.Acc_Airport.RemoveRange(context.Acc_Airport.Where(x => x.AccommodationlID == model.AccommodationId).ToList());
                     context.Acc_Airport.Add(new Acc_Airport
                     {
                         AccommodationlID = model.AccommodationId,
                         CityId = model.CityId,
                         CityName = model.CityName,
                         lastUpdate = model.LastUpdate,
                         CountryCode = model.CountryCode,
                         DestinationCode = model.DestinationCode,
                         destinationId = Convert.ToInt64(model.DestinationId),
                         rating = model.Rating,
                         category = model.Category,
                         AirPort_Code = airport.iata,
                     });
                 });
                Parallel.ForEach(model.AlternativeNames, altName =>
                 {
                     context.AccommodationAlternativeNames.RemoveRange(context.AccommodationAlternativeNames.Where(x => x.AccommodationID == model.AccommodationId).ToList());
                     context.AccommodationAlternativeNames.Add(new AccommodationAlternativeName
                     {
                         AccommodationID = model.AccommodationId,
                         AlternativeName = altName.Name,
                         EffectiveDate = altName.EffectiveDate,
                         AlternativeNameType = altName.Type,
                     });
                 });
                message = null;
                if (context.SaveChangesAsync().Result > 0)
                {
                    message = null;
                    return true;
                }
                message = "Map GIATA Data to Database failed.";
                return false;
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
        public bool DeactiveAccommodation(long id, out string message, long movedId = 0, bool deactive = false)
        {
            try
            {
                var accommodation = context.DeActiveAccommodations.FirstOrDefault(x => x.AccommodationlD == id);
                message = null;
                if (deactive)
                {
                    if (accommodation != null)
                    {
                        accommodation.ISDeactive = true;
                        accommodation.Counter = 0;
                    }
                    else
                    {
                        context.DeActiveAccommodations.Add(new DeActiveAccommodation
                        {
                            AccommodationlD = id,
                            Counter = 0,
                            ISDeactive = true,
                        });
                    }
                    return context.SaveChanges() > 0 ? true : false;
                }
                if (movedId != 0)
                {
                    if (accommodation == null)
                    {
                        context.DeActiveAccommodations.Add(new DeActiveAccommodation
                        {
                            AccommodationlD = id,
                            MovedToAccommodationlD = movedId,
                            ISDeactive = true,
                        });
                    }
                    else
                    {
                        accommodation.ISDeactive = true;
                        accommodation.MovedToAccommodationlD = movedId;
                    }
                    context.SaveChanges();
                    return context.SaveChanges() > 0 ? true : false;
                }
                return false;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
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
                var address = sourceModel.property.addresses.address;
                resultModel = new GIATADbTransferModel
                {
                    AccommodationId = Convert.ToInt64(sourceModel.property.giataId),
                    Address = address.addressLine[0].Value + " " + address.addressLine[1].Value,
                    AirportCode = sourceModel.property.airports.airport[0].iata,
                    Category = sourceModel.property.category,
                    ChainId = Convert.ToInt64(sourceModel.property.chains.chain.chainId),
                    CityId = Convert.ToInt64(sourceModel.property.city.cityId),
                    CityName = sourceModel.property.city.text,
                    CountryCode = sourceModel.property.country,
                    CountryId = 0,
                    CountryName = sourceModel.property.country,
                    Email = sourceModel.property.emails.email,
                    Fax = sourceModel.property.phones.phone.First(x => x.tech == "fax").text,
                    Latitude = sourceModel.property.geoCodes.geoCode.latitude,
                    Longitude = sourceModel.property.geoCodes.geoCode.longitude,
                    LastUpdate = sourceModel.property.lastUpdate,
                    Name = sourceModel.property.name,
                    Rating = sourceModel.property.ratings.rating.value,
                    Url = sourceModel.property.urls.url,
                    Telephone = sourceModel.property.phones.phone.First(x => x.tech == "voice").text,
                    DestinationId = sourceModel.property.destination.destinationId,
                    DestinationCode = sourceModel.property.destination.text,
                    AlternativeNames = sourceModel.property.alternativeNames.alternativeName.ToList().Select(x => new AlternativeName
                    {
                        EffectiveDate = DateTime.Parse(x.effectiveDate),
                        Name = x.text,
                        Type = x.alternativeNameType,
                    }).ToList(),
                    Suppliers = sourceModel.property.propertyCodes.provider.ToList().Select(x => new AccommodationSupplier
                    {
                        Active = x.code.status == null ? false : true,
                        Code = x.code.value.ToString(),
                        ProviderCode = x.providerCode,
                        ProviderType = x.providerType,
                        ProviderValue = x.code.value.ToString(),
                    }).ToList(),
                    Airports = sourceModel.property.airports.airport.ToList().Select(x => new Service.ServiceModel.GIATAModels.Airport
                    {
                        iata = x.iata
                    }).ToList(),
                };
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

        public bool Map(long id, out string message)
        {
            try
            {
                var serviceResult = giataAccess.GetPropertiesById(id);
                if (serviceResult.Success)
                {
                    if (serviceResult.Model is Properties)
                    {
                        if (MapGIATADataToModel((Properties)serviceResult.Model, out GIATADbTransferModel mapDb, out message))
                        {
                            if (MapGIATADataToDb(mapDb, out message))
                                return true;
                            return false;
                        }
                        return false;
                    }
                    else
                    {
                        var str = ((Error)serviceResult.Model).description.xlinkhref;
                        var movedId = Convert.ToInt64(str.Substring(str.LastIndexOf('/') + 1));
                        if (DeactiveAccommodation(id, out message, movedId))
                        {
                            message = ((Error)serviceResult.Model).description.text;
                            return true;
                        }
                        return false;
                    }
                }
                message = serviceResult.Error.Text;
                return false;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Map Range Data from GIATA Service to Db
        /// </summary>
        /// <param name="from">from giata id</param>
        /// <param name="to">to giata id</param>
        /// <returns>giata ids that failed</returns>
        public Dictionary<long, string> MapRange(long from, long to)
        {
            if (from > to)
                throw new ArgumentException("Invalid parameter(s).");
            var returnList = new Dictionary<long, string>();
            for (long i = from; i <= to; i++)
            //Parallel.For(from, to, i =>
            {
                if (!Map(i, out string message))
                    if (!TryMap(i, out message))
                        returnList.Add(i, message);

            }
            return returnList;
        }
        /// <summary>
        /// Try Map 3 attempts
        /// </summary>
        /// <param name="id"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool TryMap(long id, out string message)
        {
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Map(id, out message))
                        return true;
                }

                DeactiveAccommodation(id, out message, deactive: true);
                message = "Failed.";
                return false;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
    }
}
