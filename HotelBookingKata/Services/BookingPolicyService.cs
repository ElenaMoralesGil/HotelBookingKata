using HotelBookingKata.Entities;

namespace HotelBookingKata.Services;
public interface BookingPolicyService
{
    bool IsBookingAllowed(string employeeId, RoomType roomType);

}

