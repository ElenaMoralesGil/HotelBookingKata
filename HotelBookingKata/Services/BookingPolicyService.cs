using HotelBookingKata.Entities;

namespace HotelBookingKata.Services;
public interface BookingPolicyService
{
    void SetCompanyPolicy(string companyId, List<RoomType> roomTypes);
    void SetEmployeePolicy(string employeeId, List<RoomType> roomTypes);
    bool IsBookingAllowed(string companyId, string employeeId, RoomType roomType);

}

