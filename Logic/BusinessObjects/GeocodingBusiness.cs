using Data.DataModel;
using Service.ServiceModel.GoogleMapModels;
using Service.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.BusinessObjects
{
    public class GeocodingBusiness
    {
        private readonly GoogleMapAccess googleMapAccess = new GoogleMapAccess();

        private bool MapToDb(long id, GeocodeData data)
        {
            try
            {
                using (var context = new Entities())
                {
                    var accommodation = context.AccommodationLatLongs.FirstOrDefault(x => x.AccommodationlID == id);
                    if (accommodation != null)
                    {
                        accommodation.PlaceId = data.PlaceId;
                        accommodation.FormattedAddress = data.FormattedAddress;
                        accommodation.Latitude = data.Latitude;
                        accommodation.Longitude = data.Longitude;
                        accommodation.LocationType = string.Join(",", data.Types);
                        accommodation.IsValid = data.Types.Contains("lodging") || data.Types.Contains("point_of_interest") || data.Types.Contains("restaurant") || data.Types.Contains("food") ? true : false;
                        return context.SaveChanges() > 0 ? true : false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private (bool success, string message) GetGeocode(long id)
        {
            try
            {
                using (var context = new Entities())
                {
                    var accommodation = context.AccommodationLatLongs.FirstOrDefault(x => x.AccommodationlID == id);
                    if (accommodation != null)
                    {
                        string parameters = $"address={accommodation.Name}+{accommodation.LocationNameLong.Replace(",", " ")}";
                        var result = googleMapAccess.GetGeoCode(parameters);
                        if (result.Success)
                        {
                            if (!MapToDb(id, result.Data.First()))
                            {
                                return (false, "Map to Database failed.");
                            }
                            return (true, string.Empty);
                        }
                        return (false, result.Error.Text);
                    }
                    return (false, "Accommodation was not found.");
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public List<GetGeocodeResult> MapGeocodeRange(long from, long to)
        {
            var result = new List<GetGeocodeResult>();
            Parallel.For(from, to + 1, i =>
                {
                    try
                    {
                        var mapResult = GetGeocode(i);
                        result.Add(new GetGeocodeResult
                        {
                            Id = i,
                            Message = mapResult.message,
                            Success = mapResult.success
                        });
                    }
                    catch (Exception ex)
                    {
                        result.Add(new GetGeocodeResult
                        {
                            Id = i,
                            Success = false,
                            Message = ex.Message
                        });
                    }
                });
            return result;
        }

        public List<GetGeocodeResult> MapGeocodeAll()
        {
            var accommodationList = new List<long>();
            var result = new List<GetGeocodeResult>();
            using (var context = new Entities())
                accommodationList = context.AccommodationLatLongs.Select(x => x.AccommodationlID).ToList();
            Parallel.ForEach(accommodationList, id =>
             {
                 try
                 {
                     var mapResult = GetGeocode(id);
                     result.Add(new GetGeocodeResult
                     {
                         Id = id,
                         Message = mapResult.message,
                         Success = mapResult.success
                     });
                 }
                 catch (Exception ex)
                 {
                     result.Add(new GetGeocodeResult
                     {
                         Id = id,
                         Success = false,
                         Message = ex.Message
                     });
                 }
             });
            return result;
        }
    }
}
