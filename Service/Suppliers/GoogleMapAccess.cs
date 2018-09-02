using Common;
using Service.ServiceModel.GoogleMapModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Suppliers
{
    public class GoogleMapAccess
    {
        private enum OutputFormat
        {
            json,
            xml
        }
        private string GetValidUrl(OutputFormat outputFormat, string parameters) => $"http://maps.google.com/maps/api/geocode/{outputFormat}?{parameters}";

        public ServiceResutl GetGeoCode(string parameters)
        {
            try
            {
                var url = GetValidUrl(OutputFormat.json, parameters);
                var response = Utility.GetJson<ServiceResponse>(url);
                if (response.status == "OK")
                {
                    return new ServiceResutl
                    {
                        Success = true,
                        Data = response.results.Select(x => new GeocodeData
                        {
                            FormattedAddress = x.formatted_address,
                            Latitude = x.geometry.location.lat.ToString(),
                            Longitude = x.geometry.location.lng.ToString(),
                            PlaceId = x.place_id,
                            Types = x.types,
                            Status = response.status,
                        }).ToList()
                    };
                }
                if (response.status == "ZERO_RESULTS")
                {
                    return new ServiceResutl
                    {
                        Success = true,
                        Data = new List<GeocodeData>(),
                    };
                }
                if (response.status == "INVALID_REQUEST" || response.status == "REQUEST_DENIED")
                {
                    return new ServiceResutl
                    {
                        Success = false,
                        Error = new ServiceModel.PublicModels.Error
                        {
                            Text = "Invalid request.",
                        }
                    };
                }
                if (response.status == "UNKNOWN_ERROR")
                {
                    var tryResponse = Utility.GetJson<ServiceResponse>(url);
                    if (tryResponse.status == "OK")
                    {
                        return new ServiceResutl
                        {
                            Success = true,
                            Data = tryResponse.results.Select(x => new GeocodeData
                            {
                                FormattedAddress = x.formatted_address,
                                Latitude = x.geometry.location.lat.ToString(),
                                Longitude = x.geometry.location.lng.ToString(),
                                PlaceId = x.place_id,
                                Types = x.types,
                                Status = tryResponse.status,
                            }).ToList()
                        };
                    }
                }
                return new ServiceResutl
                {
                    Success = false,
                    Error = new ServiceModel.PublicModels.Error
                    {
                        Text = "Send request failed.",
                    }
                };

            }
            catch (Exception ex)
            {
                return new ServiceResutl
                {
                    Error = new ServiceModel.PublicModels.Error
                    {
                        Text = ex.Message,
                    },
                    Success = false,
                };
            }
        }

    }
}
