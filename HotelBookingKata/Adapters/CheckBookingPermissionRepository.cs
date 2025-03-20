using HotelBookingKata.Entities;
namespace HotelBookingKata.Adapters;

public interface CheckBookingPermissionRepository
{
    Task<bool> IsBookingAllowed(string employeeId, RoomType roomType);
}
