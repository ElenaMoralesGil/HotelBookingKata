using HotelBookingKata.Entities;
namespace HotelBookingKata.Repositories
{
    public interface BookingPolicyRepository
    {
        void SetCompanyPolicy(string companyId, List<RoomType> roomTypes);
        void SetEmployeePolicy(string employeeId, List<RoomType> roomTypes);
        bool HasCompanyPolicy(string companyId);
        bool HasEmployeePolicy(string employeeId);
        bool IsRoomTypeAllowedForCompany(string companyId, RoomType roomType);
        bool IsRoomTypeAllowedForEmployee(string employeeId, RoomType roomType);


    }
}
