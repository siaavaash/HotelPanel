using Data;
using Data.ViewModel.VerifyPanelViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic.BusinessObjects
{
    public class VerifyPanelBusiness
    {
        public HotelInfoViewModel GetAccommodationInfo(long accommodationId)
        {
            try
            {
                return DataContext.Context.Accommodations.Where(x => x.AccommodationlID == accommodationId).Select(x => new HotelInfoViewModel
                {
                    AccommodationId = x.AccommodationlID,
                    Address = x.Address,
                    Email = x.Email,
                    Fax = x.Fax,
                    IsError = x.IsError,
                    IsVerified = x.IsVerified,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    Telephone = x.Telephone,
                    Url = x.Url,
                    VerifiedDate = x.DateVerified,
                    Rating = x.Rating,
                    Name = x.Name,
                    IsActive = x.IsActive,
                    Description = x.Description,
                }).FirstOrDefault() ?? throw new Exception("The Accommodation does not exist.");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Image> GetRoomImages(long accommodationId, bool verified, bool reported, bool activated)
        {
            try
            {
                return DataContext.Context.AccomodationRoomImages.Where(x => x.AccommodationID == accommodationId && x.IsActive == activated && x.IsReported == reported && x.IsVerified == verified).Select(x => new Image
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
            catch (Exception)
            {

                throw;
            }
        }
        public List<Image> GetAccommodationImages(long accommodationId, bool verified, bool reported, bool activated)
        {
            try
            {
                return DataContext.Context.AccomodationImages.Where(x => x.AccommodationlID == accommodationId && x.IsActive == activated && x.IsReported == reported && x.IsVerified == verified).Select(x => new Image
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
            catch (Exception)
            {
                throw;
            }
        }
        public List<Facility> GetAccommodationFacilities(long accommodationId)
        {
            try
            {
                return DataContext.Context.Accommodations.FirstOrDefault(x => x.AccommodationlID == accommodationId)?.Facilities.Select(x => new Facility
                {
                    AccommodationId = accommodationId,
                    Category = x.Category,
                    Id = x.FacilityID,
                    Name = x.Name
                }).ToList() ?? throw new Exception("The Accommodation does not exist.");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
