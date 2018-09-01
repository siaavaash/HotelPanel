using Data.DataModel;
using Service.ServiceModel;
using Service.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public List<GetGeocodeResult> GetGeocodeRange(long from, long to)
        {
            try
            {
                var result = new List<GetGeocodeResult>();
                Parallel.For(from, to + 1, i =>
                    {

                    });
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
