using BookingDB;
using HtmlAgilityPack;
using Service.ServiceModel.BookingModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Suppliers
{
    public class BookingAccess
    {
        private static IPStatus PingService(string url)
        {
            Uri uri = new Uri(url);
            Ping ping = new Ping();
            PingReply reply = ping.Send(uri.Host);
            return reply.Status;
        }
        public static async Task<(BookingResponseModel<HotelData> hotelRes, BookingResponseModel<List<RoomData>> roomRes)> GetDataAsync(HttpClient client, string url)
        {
            try
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(await response.Content.ReadAsStringAsync());
                    return (GetHotelInfo(doc), GetRooms(doc));
                }
                Thread.Sleep(TimeSpan.FromMinutes(2));
                DateTime timeout = DateTime.Now;
                while ((response.StatusCode == System.Net.HttpStatusCode.GatewayTimeout
                    || response.StatusCode == System.Net.HttpStatusCode.BadGateway
                    || response.StatusCode == System.Net.HttpStatusCode.NotImplemented
                    || response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable
                    || response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    && DateTime.Now - timeout < TimeSpan.FromSeconds(60))
                {
                    response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        HtmlDocument doc = new HtmlDocument();
                        doc.LoadHtml(await response.Content.ReadAsStringAsync());
                        return (GetHotelInfo(doc), GetRooms(doc));
                    }
                }
                throw new Exception();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " Load HtmlWeb Failed.");
            }
        }

        // Get Hotel Info
        private static BookingResponseModel<HotelData> GetHotelInfo(HtmlDocument doc)
        {
            try
            {
                // Get Name
                var name = doc.DocumentNode.Descendants().FirstOrDefault(x => x.Id == "hp_hotel_name").InnerText.Replace("\n", "");

                // Get Address
                var address = doc.DocumentNode.Descendants().FirstOrDefault(x => x.HasClass("hp_address_subtitle")).InnerText.Replace("\n", "");

                // Get Description
                var paragraphes = doc.DocumentNode.Descendants().FirstOrDefault(x => x.Attributes["id"]?.Value == "summary")?.Descendants("p");
                var description = string.Join(" ", paragraphes.Select(x => x.InnerText).ToList());

                // Get Image Urls
                var imageUrls = doc.DocumentNode.Descendants("img")?.Where(x => x.Attributes["data-highres"]?.Value != null).Select(x => x.Attributes["src"]?.Value ?? x.Attributes["data-lazy"]?.Value).ToList();

                // Get Latitude & Longitude
                var mapStyle = doc.DocumentNode.Descendants("a").FirstOrDefault(x => x.Attributes["data-source"]?.Value == "map_thumbnail")?.Attributes["style"]?.Value;
                var googleMapUrl = mapStyle.Substring(mapStyle.IndexOf("("), mapStyle.LastIndexOf(")") - mapStyle.IndexOf("("));
                var center = googleMapUrl.Substring(googleMapUrl.IndexOf("center"));
                var lat_long = center.Substring(7, center.IndexOf("&") - 7);
                var latitude = lat_long.Split(',')[0];
                var longitude = lat_long.Split(',')[1];

                // Get Location
                var counter = 0;
                var breadcrumbList = doc.DocumentNode.Descendants("a").Where(x => x.HasClass("bui_breadcrumb__link")).ToList();
                var locations = breadcrumbList.Select(x => new Location { Name = x.InnerText.Replace("\n", ""), LocationTypeID = ++counter }).ToList();

                // Get Good to Know
                var checkin = doc.DocumentNode.Descendants().FirstOrDefault(x => x.Id == "checkin_policy")?.InnerText.Replace("\n", "").Replace("Check-in", "").Replace("&nbsp;", " ");
                var checkout = doc.DocumentNode.Descendants().FirstOrDefault(x => x.Id == "checkout_policy")?.InnerText.Replace("\n", "").Replace("Check-out", "").Replace("&nbsp;", " ");
                var pets = doc.DocumentNode.Descendants().FirstOrDefault(x => x.Id == "hotelPoliciesInc")?.ChildNodes.FirstOrDefault(q => q.Name == "div" && q.InnerHtml.Contains("Pets"))?.InnerText.Replace("\nPets\n", "").Replace("\n", "");

                // Get Facilities
                var facilities = new List<Facility>();
                var facilityBoxes = doc.DocumentNode.Descendants("div").Where(x => x.HasClass("facilitiesChecklistSection")).ToList();
                facilityBoxes.ForEach(facility =>
                {
                    var category = facility.ChildNodes["h5"]?.InnerText.Replace("\n", "");
                    var items = facility.Descendants("li").ToList();
                    foreach (var item in items)
                        facilities.Add(new Facility
                        {
                            Category = category,
                            Title = item.InnerText.Replace("\n", ""),
                        });
                });
                return new BookingResponseModel<HotelData>
                {
                    Success = true,
                    Data = new HotelData
                    {
                        Name = name,
                        Address = address,
                        Description = description,
                        Facilities = facilities,
                        GoodToKnow = new GoodToNow
                        {
                            Pets = pets,
                            CheckOut = checkout,
                            CheckIn = checkin,
                        },
                        Latitude = latitude,
                        Locations = locations,
                        Longitude = longitude,
                        HotelImageUrls = imageUrls,
                    }
                };

            }
            catch (Exception ex)
            {
                return new BookingResponseModel<HotelData>
                {
                    Success = false,
                    Errors = new List<ServiceModel.PublicModels.Error>
                    {
                        new ServiceModel.PublicModels.Error
                        {
                            Text = ex.Message
                        }
                    }
                };
            }
        }

        // Get Rooms Info
        private static BookingResponseModel<List<RoomData>> GetRooms(HtmlDocument doc)
        {
            try
            {
                var roomUrls = GetRoomsUrl(doc);
                var infos = new ConcurrentBag<RoomData>();
                var result = new BookingResponseModel<List<RoomData>>
                {
                    Success = true,
                    Data = new List<RoomData>(),
                };
                Parallel.ForEach(roomUrls, roomUrl =>
                {
                    infos.Add(GetRoomInfo(doc, roomUrl.Key, roomUrl.Value));
                });
                result.Data.AddRange(infos.ToList());
                return result;
            }
            catch (Exception ex)
            {
                return new BookingResponseModel<List<RoomData>>
                {
                    Success = false,
                    Errors = new List<ServiceModel.PublicModels.Error>
                    {
                        new ServiceModel.PublicModels.Error
                        {
                            Text = ex.Message
                        }
                    }
                };
            }
        }

        private static Dictionary<string, RoomData> GetRoomsUrl(HtmlDocument doc)
        {
            try
            {
                var result = new Dictionary<string, RoomData>();
                var noLinkId = 100;
                var trs = doc.DocumentNode.Descendants().FirstOrDefault(x => x.Id == "maxotel_rooms")?.ChildNodes["tbody"]?.Descendants("tr")?.Where(x => !x.HasClass("extendedRow")).ToList();
                trs.ForEach(tr =>
                {
                    var adults = tr.Descendants().FirstOrDefault(x => x.HasClass("occupancy_adults"))?.Descendants("i");
                    var childs = tr.Descendants().FirstOrDefault(x => x.HasClass("occupancy_children"))?.Descendants("i");
                    var manyAdult = byte.TryParse(tr.Descendants().FirstOrDefault(x => x.HasClass("occupancy_multiplier_number"))?.InnerText, out byte multiplier);
                    string sleeps = "";
                    if (manyAdult)
                        for (int i = 1; i <= 6; i++) { sleeps += "A"; }
                    else
                        sleeps = childs != null ? string.Join("", adults.Select(x => "A").ToArray().Concat(childs?.Select(x => "C").ToArray())) : string.Join("", adults.Select(x => "A").ToArray());
                    var title = tr.Descendants().FirstOrDefault(x => x.HasClass("jq_tooltip"))?.Attributes["title"]?.Value.Replace("<br/>", " ");
                    var link = tr.Descendants().FirstOrDefault(x => x.HasClass("togglelink") && !x.HasClass("disabled"))?.Attributes["href"]?.Value.Replace("#", "") ?? $"{++noLinkId}";
                    var name = tr.Descendants().FirstOrDefault(x => x.Attributes["data-room-name-en"] != null)?.Attributes["data-room-name-en"]?.Value;
                    var info = tr.Descendants().FirstOrDefault(x => x.Attributes["class"]?.Value.Contains("bed-types-wrapper") ?? false)?.Descendants("ul").Select(x => x.InnerText.Replace("\n", "")).ToArray();
                    result.Add($"{link}", new RoomData
                    {
                        Sleeps = sleeps,
                        SleepsInfo = title,
                        RoomTypeID = link,
                        RoomType = name,
                        RoomTypeInfo = info != null ? string.Join(" ", info) : "",
                    });
                });
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private static RoomData GetRoomInfo(HtmlDocument doc, string id, RoomData data)
        {
            try
            {
                var container = doc.DocumentNode.Descendants().FirstOrDefault(x => x.Id == $"blocktoggle{id}");

                // Get Room Images
                var imgs = container?.Descendants().FirstOrDefault(x => x.HasClass("hp-gallery"))?.Descendants("img");

                // Get Facilities
                var facilities = container?.Descendants().Where(x => x.HasClass("hp_rt_lightbox_facilities__list__item"));

                // Get Description
                var description = container?.Descendants("p").FirstOrDefault(x => x.HasClass("js_hp_rt_lightbox_room_desc"))?.InnerText.Replace("\n", "");

                // Get Room Size
                var size = container?.Descendants().FirstOrDefault(x => x.Attributes["data-name-en"]?.Value == "roomsize")?.InnerText.Replace("Room Size", "").Replace("\n", "");

                var counter = 0;

                data.Description = description;
                data.Facilities = facilities?.Select(x => x.Attributes["data-name-en"]?.Value.Replace("&#47;", "/")).ToList() ?? new List<string>();
                data.RoomImages = imgs?.Select(x => new RoomImage
                {
                    Url = x.Attributes["data-lazy"].Value,
                    ID = ++counter,
                }).ToList() ?? new List<RoomImage>();
                data.RoomSize = size;

                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
