using Service.ServiceModel.DOTWModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Service.Suppliers
{
    public enum Command
    {
        getcurrenciesids = 1,
        getallcities = 2,
        getallcountries = 3,
        getlanguagesids = 4,
        getleisureids = 5,
        getbusinessids = 6,
        getamenitieids = 7,
        getroomamenitieids = 8,
        getsalutationsids = 9,
        gethotelchainsids = 10,
        getratebasisids = 11,
        getairlinesids = 12,
    }

    public class DOTWAccess
    {
        private const string Url = "http://us.dotwconnect.com/gatewayV3.dotw";

        private static string GeneralXmlReqByCommand(Command command) => $"<customer><username>Mehrdad</username><password>e3bb7ee8b41c04d9d94b82c74ee1325d</password><id>1080498</id><source>1</source><request command=\"{command.ToString()}\"></request></customer>";

        private static string SearchHotelReqXml(int cityCode) => $"<customer><username>Mehrdad</username><password>e3bb7ee8b41c04d9d94b82c74ee1325d</password><id>1080498</id><source>1</source><product>hotel</product><request command=\"searchhotels\"><bookingDetails><fromDate>{DateTime.Now.Date.ToString("yyyy-MM-dd")}</fromDate><toDate>{DateTime.Now.AddDays(1).Date.ToString("yyyy-MM-dd")}</toDate><currency>520</currency><rooms no=\"1\"><room runno=\"0\"><adultsCode>1</adultsCode><children no=\"0\"></children><rateBasis>-1</rateBasis></room></rooms></bookingDetails><return><getRooms>true</getRooms><filters xmlns:a=\"http://us.dotwconnect.com/xsd/atomicCondition\" xmlns:c=\"http://us.dotwconnect.com/xsd/complexCondition\"><city>{cityCode}</city><noPrice>true</noPrice></filters><fields><field>priority</field><field>preferred</field><field>builtYear</field><field>renovationYear</field><field>floors</field><field>noOfRooms</field><field>preferred</field><field>luxury</field><field>fullAddress</field><field>description1</field><field>description2</field><field>hotelName</field><field>address</field><field>zipCode</field><field>location</field><field>locationId</field><field>location1</field><field>location2</field><field>location3</field><field>cityName</field><field>cityCode</field><field>stateName</field><field>stateCode</field><field>countryName</field><field>countryCode</field><field>regionName</field><field>regionCode</field><field>attraction</field><field>amenitie</field><field>leisure</field><field>business</field><field>transportation</field><field>hotelPhone</field><field>hotelCheckIn</field><field>hotelCheckOut</field><field>minAge</field><field>rating</field><field>images</field><field>fireSafety</field><field>hotelPreference</field><field>direct</field><field>geoPoint</field><field>leftToSell</field><field>chain</field><field>lastUpdated</field><field>priority</field><roomField>name</roomField><roomField>roomInfo</roomField><roomField>roomAmenities</roomField><roomField>twin</roomField></fields></return></request></customer>";

        public static string GetInternalCodes(Command command)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(GeneralXmlReqByCommand(command), Encoding.UTF8, "application/xml");
                    var response = client.PostAsync(Url, content).Result;
                    if (response.IsSuccessStatusCode)
                        return response.Content.ReadAsStringAsync().Result;
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<string> SearchHotelByCityAsync(int cityCode)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMinutes(2);
                    var content = new StringContent(SearchHotelReqXml(cityCode), Encoding.UTF8, "application/xml");
                    var response = await client.PostAsync(Url, content);
                    if (response.IsSuccessStatusCode)
                        return await response.Content.ReadAsStringAsync();
                    else
                    {
                        response = await client.PostAsync(Url, content);
                        if (response.IsSuccessStatusCode)
                            return await response.Content.ReadAsStringAsync();
                        else
                        {
                            response = await client.PostAsync(Url, content);
                            if (response.IsSuccessStatusCode)
                                return await response.Content.ReadAsStringAsync();
                            return null;
                        }
                    }
                }
            }
            catch (Exception)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.Timeout = TimeSpan.FromMinutes(1);
                        var content = new StringContent(SearchHotelReqXml(cityCode), Encoding.UTF8, "application/xml");
                        var response = await client.PostAsync(Url, content);
                        if (response.IsSuccessStatusCode)
                            return await response.Content.ReadAsStringAsync();
                        else
                        {
                            response = await client.PostAsync(Url, content);
                            if (response.IsSuccessStatusCode)
                                return await response.Content.ReadAsStringAsync();
                            else
                            {
                                response = await client.PostAsync(Url, content);
                                if (response.IsSuccessStatusCode)
                                    return await response.Content.ReadAsStringAsync();
                                return null;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public static DOTWModels.CountriesModel.result GetAllCountries()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(DOTWModels.CountriesModel.result));
                string content = GetInternalCodes(Command.getallcountries);
                if (content == null) return null;
                TextReader reader = new StringReader(GetInternalCodes(Command.getallcountries));
                return (DOTWModels.CountriesModel.result)serializer.Deserialize(reader);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static CitiesByCountryModel GetCitiesByCountry(int countryCode, string countryName)
        {
            try
            {
                string request = $"<customer><username>Mehrdad</username><password>e3bb7ee8b41c04d9d94b82c74ee1325d</password><id>1080498</id><source>1</source><request command=\"getallcities\"><return><filters><countryCode>{countryCode}</countryCode></filters></return></request></customer> ";
                XmlSerializer serializer = new XmlSerializer(typeof(DOTWModels.CitiesModel.result));
                using (var client = new HttpClient())
                {
                    var content = new StringContent(request);
                    var response = client.PostAsync(Url, content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        TextReader reader = new StringReader(response.Content.ReadAsStringAsync().Result);
                        return new CitiesByCountryModel
                        {
                            CountryCode = countryCode,
                            CountryName = countryName,
                            Cities = ((DOTWModels.CitiesModel.result)serializer.Deserialize(reader)).cities.city
                        };
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
