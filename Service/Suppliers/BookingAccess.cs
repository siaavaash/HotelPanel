using HtmlAgilityPack;
using Service.ServiceModel.BookingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Suppliers
{
    public class BookingAccess
    {
        // Get Hotel Info
        public BookingResponseModel<HotelData> GetHotelInfo(string url)
        {
            try
            {
                var web = new HtmlWeb();
                var doc = web.Load(url);

                // Get Description
                var paragraphes = doc.DocumentNode.Descendants().FirstOrDefault(x => x.Attributes["id"].Value == "summery").Descendants("p");
                var description = string.Join(" ", paragraphes.Select(x => x.InnerText).ToList());

                // Get Image Urls
                var imageUrls = doc.DocumentNode.Descendants("img").Where(x => x.Attributes["data-highres"].Value != null).Select(x => x.Attributes["src"].Value).ToList();

                // Get Latitude & Longitude
                var mapStyle = doc.DocumentNode.Descendants("a").FirstOrDefault(x => x.Attributes["data-source"].Value == "map_thumbnail").Attributes["style"].Value;
                var googleMapUrl = mapStyle.Substring(mapStyle.IndexOf("("), mapStyle.LastIndexOf(")") - mapStyle.IndexOf("("));
                var center = googleMapUrl.Substring(googleMapUrl.IndexOf("center"));
                var lat_long = center.Substring(0, center.IndexOf("&"));
                var latitude = lat_long.Split(',')[0];
                var longitude = lat_long.Split(',')[1];

                // Get Location
                var counter = 0;
                var breadcrumbList = doc.DocumentNode.Descendants("ol").FirstOrDefault(x => x.Attributes["typeof"].Value == "BreadcrumbList").ChildNodes;
                var locations = breadcrumbList.Select(x => new Location { Name = x.Descendants("a").FirstOrDefault().InnerText, LocationID = ++counter }).ToList();

                // Get Good to Know
                var checkin = doc.DocumentNode.SelectSingleNode(".//*[@id= 'checkin_policy']").InnerText;
                var checkout = doc.DocumentNode.SelectSingleNode(".//*[@id= 'checkout_policy']").InnerText;
                checkin = checkin.Replace("Check-in", "").Replace("\n", "").Replace("&nbsp;", " ");
                checkout = checkout.Replace("Check-out", "").Replace("\n", "").Replace("&nbsp;", " ");
                var pets = doc.DocumentNode.SelectSingleNode(".//*[@id= 'hotelPoliciesInc']").ChildNodes.FirstOrDefault(q => q.Name == "div" && q.InnerHtml.Contains("Pets")).InnerText;
                pets = pets.Replace("\nPets\n", "").Replace("\n", "");

                // Get Facilities
                var facilities = new List<Facility>();
                var facilityBoxes = doc.DocumentNode.Descendants("div").Where(x => x.HasClass("facilitiesChecklistSection")).ToList();
                facilityBoxes.ForEach(facility =>
                {
                    var category = facility.ChildNodes["h5"].InnerText;
                    var items = facility.ChildNodes["ul"].ChildNodes;
                    foreach (var item in items)
                        facilities.Add(new Facility
                        {
                            Category = category,
                            Title = item.InnerText
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
        public BookingResponseModel<List<RoomData>> GetRooms(string hotelUrl)
        {
            try
            {
                var roomUrls = GetRoomsUrl(hotelUrl);
                var result = new BookingResponseModel<List<RoomData>>
                {
                    Success = true,
                    Data = new List<RoomData>(),
                };
                Parallel.ForEach(roomUrls, roomUrl =>
                {
                    result.Data.Add(GetRoomInfo(roomUrl.Key, roomUrl.Value));
                });
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

        public Dictionary<string, RoomData> GetRoomsUrl(string hotelUrl)
        {
            try
            {
                var web = new HtmlWeb();
                var doc = web.Load(hotelUrl);

                var result = new Dictionary<string, RoomData>();

                var links = doc.DocumentNode.Descendants("a").Where(x => x.HasClass("jqrt togglelink"));
                var trs = doc.DocumentNode.Descendants().FirstOrDefault(x => x.Id == "maxotel_rooms").ChildNodes["tbody"].Descendants("tr").Where(x => !x.HasClass("extendedRow sold")).ToList();
                trs.ForEach(tr =>
                {
                    var adults = tr.Descendants().FirstOrDefault(x => x.HasClass("occupancy_adults")).Descendants("i");
                    var childs = tr.Descendants().FirstOrDefault(x => x.HasClass("occupancy_children")).Descendants("i");
                    var sleeps = string.Join("", adults.Select(x => "A").ToArray().Concat(childs.Select(x => "C").ToArray()));
                    var title = tr.Descendants().FirstOrDefault(x => x.HasClass("jq_tooltip")).Attributes["data-title"].Value;
                    var link = tr.Descendants().FirstOrDefault(x => x.HasClass("jqrt togglelink")).Attributes["href"].Value.Replace("#", "");
                    var name = tr.Descendants().FirstOrDefault(x => x.HasClass("jqrt togglelink")).Attributes["data-room-name-en"].Value;
                    var info = tr.Descendants().FirstOrDefault(x => x.HasClass("bed-types-wrapper")).ChildNodes.Select(x => x.InnerText).ToArray();
                    result.Add($"{hotelUrl}#rt-lightbox-open-{link}", new RoomData
                    {
                        Sleeps = sleeps,
                        SleepsInfo = title,
                        RoomTypeID = link,
                        RoomType = name,
                        RoomTypeInfo = string.Join(" ", info),
                    });
                });
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public RoomData GetRoomInfo(string url, RoomData data)
        {
            try
            {
                var web = new HtmlWeb();
                var doc = web.Load(url);

                var container = doc.DocumentNode.Descendants("div").FirstOrDefault(x => x.HasClass("hp_rt_lightbox_wrapper"));

                // Get Room Images
                var imgs = container.Descendants("img").Where(x => x.ParentNode.Name == "div").ToList();

                // Get Facilities
                var facilities = container.Descendants("li").Where(x => x.HasClass("hp_rt_lightbox_facilities__list__item")).ToList();

                // Get Description
                var description = container.Descendants("p").FirstOrDefault(x => x.HasClass("js_hp_rt_lightbox_room_desc")).InnerText;

                // Get Room Size
                var size = container.Descendants().FirstOrDefault(x => x.Attributes["data-name-en"].Value == "roomsize").InnerText;

                var counter = 0;

                data.Description = description;
                data.Facilities = facilities.Select(x => x.InnerText).ToList();
                data.RoomImages = imgs.Select(x => new RoomImage
                {
                    Url = x.Attributes["src"].Value,
                    ID = ++counter,
                }).ToList();
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
