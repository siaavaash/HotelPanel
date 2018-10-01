using HtmlAgilityPack;
using Service.ServiceModel.BookingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Suppliers
{
    public class BookingAccess
    {
        private readonly string hotelUrl;
        private readonly HtmlDocument doc;
        public BookingAccess(string hotelUrl)
        {
            var web = new HtmlWeb();
            doc = web.Load(hotelUrl);
            this.hotelUrl = hotelUrl;
        }

        // Get Hotel Info
        public BookingResponseModel<HotelData> GetHotelInfo()
        {
            try
            {
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
                var locations = breadcrumbList.Select(x => new Location { Name = x.InnerText.Replace("\n", ""), LocationID = ++counter }).ToList();

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
                        Description = description,
                        Facilities = facilities,
                        GoodToKnow = new GoodToKnow
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
        public BookingResponseModel<List<RoomData>> GetRooms()
        {
            try
            {
                var roomUrls = GetRoomsUrl();
                var result = new BookingResponseModel<List<RoomData>>
                {
                    Success = true,
                    Data = new List<RoomData>(),
                };
                //Parallel.ForEach(roomUrls, roomUrl =>
                foreach (var roomUrl in roomUrls)
                {
                    result.Data.Add(GetRoomInfo(roomUrl.Key, roomUrl.Value));
                }
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

        private Dictionary<string, RoomData> GetRoomsUrl()
        {
            try
            {
                var result = new Dictionary<string, RoomData>();

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
                    var link = tr.Descendants().FirstOrDefault(x => x.HasClass("togglelink") && !x.HasClass("disabled"))?.Attributes["href"]?.Value.Replace("#", "");
                    var name = tr.Descendants().FirstOrDefault(x => x.HasClass("togglelink"))?.Attributes["data-room-name-en"]?.Value;
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
        private RoomData GetRoomInfo(string id, RoomData data)
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
