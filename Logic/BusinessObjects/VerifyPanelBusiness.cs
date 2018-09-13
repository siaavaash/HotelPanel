using Data;
using Data.DataModel;
using Data.ViewModel.VerifyPanelViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic.BusinessObjects
{
    public class VerifyPanelBusiness
    {
        public List<AutocompleteViewModel> GetCities(string term) => DataContext.Context.Locations.Where(x => x.LocationTypeID == 4 && x.Name.StartsWith(term)).OrderBy(x => x.Name).Take(15).ToList().Select(x => new AutocompleteViewModel
        {
            Country = x.NameLong.Substring(x.NameLong.LastIndexOf(", ") + 2),
            Id = x.CityId ?? 0,
            Text = x.Name,
            NameLong = x.NameLong,
        }).ToList();
        public List<AutocompleteViewModel> GetCountries(string term) => DataContext.Context.Locations.Where(x => x.LocationTypeID == 2 && x.Name.StartsWith(term)).OrderBy(x => x.Name).Select(x => new AutocompleteViewModel
        {
            Id = x.LocationID,
            Text = x.Name,
            NameLong = x.NameLong,
        }).ToList();
        public List<Data.ViewModel.VerifyPanelViewModels.Facility> GetFacilities(long id) => DataContext.Context.Facilities.Where(x => !x.Accommodations.Any(y => y.AccommodationlID == id)).OrderBy(x => x.Name).Select(x => new Data.ViewModel.VerifyPanelViewModels.Facility
        {
            Category = x.Category,
            Id = x.FacilityID,
            Name = x.Name,
        }).ToList();
        public HotelInfoViewModel GetAccommodationInfo(long accommodationId)
        {
            try
            {
                using (var context = new Entities())
                {
                    var result = context.Accommodations.AsNoTracking().Where(x => x.AccommodationlID == accommodationId).ToList().Select(x => new HotelInfoViewModel
                    {
                        AccommodationId = x.AccommodationlID,
                        Address = x.Address,
                        Email = x.Email,
                        Fax = x.Fax,
                        IsVerified = x.IsVerified,
                        Latitude = x.Latitude,
                        Longitude = x.Longitude,
                        Telephone = x.Telephone,
                        Url = x.Url,
                        VerifiedDate = x.DateVerified?.ToString("dd MMM yyyy"),
                        Rating = x.Rating,
                        Name = x.Name,
                        IsActive = x.IsActive,
                        Description = x.Description,
                        BookingUrl = x.BookingUrl,
                        CityName = x.CityName,
                        CityId = x.CityId,
                        CountryName = x.Country,
                    }).FirstOrDefault() ?? throw new Exception("The Accommodation does not exist.");
                    result.AccommodationFacilities = GetAccommodationFacilities(accommodationId);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Image> GetRoomImages(long accommodationId, bool verified = true, bool reported = false, bool activated = true)
        {
            try
            {
                using (var context = new Entities())
                {
                    return context.AccomodationRoomImages.Where(x => x.AccommodationID == accommodationId && x.IsActive == activated && (x.IsReported ?? false) == reported && x.IsVerified == verified).Select(x => new Image
                    {
                        Id = x.AccomodationRoomImageID,
                        IsVerified = x.IsVerified,
                        IsReported = x.IsReported,
                        VerifiedDate = x.VerifiedDate,
                        Url = x.RoomImagePhysicalPath,
                        NumberOfReports = x.NumberOfReports,
                        IsActive = x.IsActive,
                        AccommodationId = x.AccommodationID,
                    }).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Image> GetAccommodationImages(long accommodationId, bool verified = true, bool reported = false, bool activated = true)
        {
            try
            {
                using (var context = new Entities())
                {
                    return context.AccomodationImages.Where(x => x.AccommodationlID == accommodationId && x.IsActive == activated && (x.IsReported ?? false) == reported && x.IsVerified == verified).Select(x => new Image
                    {
                        Id = x.ImageID,
                        IsVerified = x.IsVerified,
                        IsReported = x.IsReported,
                        VerifiedDate = x.VerifiedDate,
                        Url = x.Link,
                        NumberOfReports = x.NumberOfReports,
                        IsActive = x.IsActive,
                        AccommodationId = x.AccommodationlID,
                    }).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Data.ViewModel.VerifyPanelViewModels.Facility> GetAccommodationFacilities(long accommodationId)
        {
            try
            {
                using (var context = new Entities())
                {
                    return context.Accommodations.FirstOrDefault(x => x.AccommodationlID == accommodationId)?.Facilities.Select(x => new Data.ViewModel.VerifyPanelViewModels.Facility
                    {
                        AccommodationId = accommodationId,
                        Category = x.Category,
                        Id = x.FacilityID,
                        Name = x.Name
                    }).ToList() ?? throw new Exception("The Accommodation does not exist.");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Verify(long userId, HotelInfoViewModel model, List<long> newFacilities, List<long> removedFacilities, out string message)
        {
            try
            {
                using (var context = new Entities())
                {
                    if (model != null)
                    {
                        var accommodation = DataContext.Context.Accommodations.FirstOrDefault(x => x.AccommodationlID == model.AccommodationId);
                        if (accommodation == null)
                        {
                            message = "The Accommodation does not exist.";
                            return false;
                        }
                        accommodation.Name = model.Name ?? accommodation.Name;
                        accommodation.CityName = model.CityName ?? accommodation.CityName;
                        accommodation.Country = model.CountryName ?? accommodation.Country;
                        accommodation.CityId = model.CityId ?? accommodation.CityId;
                        accommodation.Address = model.Address ?? accommodation.Address;
                        accommodation.Rating = model.Rating ?? accommodation.Rating;
                        accommodation.Telephone = model.Telephone ?? accommodation.Telephone;
                        accommodation.Fax = model.Fax ?? accommodation.Fax;
                        accommodation.Email = model.Email ?? accommodation.Email;
                        accommodation.Url = model.Url ?? accommodation.Url;
                        accommodation.Description = model.Description ?? accommodation.Description;
                        accommodation.Latitude = model.Latitude ?? accommodation.Latitude;
                        accommodation.Longitude = model.Longitude ?? accommodation.Longitude;
                        accommodation.BookingUrl = model.BookingUrl ?? accommodation.BookingUrl;
                        accommodation.DateVerified = DateTime.Now.Date;
                        accommodation.IsVerified = true;
                        accommodation.UserID = userId;
                        accommodation.IsActive = model.IsActive ?? false;
                        foreach (var item in newFacilities ?? new List<long>())
                        {
                            var facility = DataContext.Context.Facilities.FirstOrDefault(x => x.FacilityID == item);
                            if (facility != null)
                                accommodation.Facilities.Add(facility);
                        }
                        foreach (var item in removedFacilities ?? new List<long>())
                        {
                            var facility = DataContext.Context.Facilities.FirstOrDefault(x => x.FacilityID == item);
                            if (facility != null)
                                accommodation.Facilities.Remove(facility);
                        }
                        foreach (var roomImage in model.RoomImages ?? new List<Image>())
                        {
                            var image = DataContext.Context.AccomodationRoomImages.FirstOrDefault(x => x.AccomodationRoomImageID == roomImage.Id);
                            if (image != null)
                            {
                                image.IsVerified = true;
                                image.IsReported = roomImage.IsReported;
                                image.IsActive = roomImage.IsActive;
                                image.VerifiedDate = DateTime.Now.Date;
                            }
                        }
                        foreach (var item in model.AccommodationImages ?? new List<Image>())
                        {
                            var image = DataContext.Context.AccomodationImages.FirstOrDefault(x => x.ImageID == item.Id);
                            if (image != null)
                            {
                                image.IsVerified = true;
                                image.IsReported = item.IsReported;
                                image.IsActive = item.IsActive;
                                image.VerifiedDate = DateTime.Now.Date;
                            }
                        }
                        message = null;
                        if (!(DataContext.Context.SaveChangesAsync().Result > 0))
                        {
                            message = "Nothing to Save.";
                            return false;
                        }
                        return true;
                    }
                    message = "Model is inavlid.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
    }
}
