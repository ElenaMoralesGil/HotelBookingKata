using HotelBookingKata.Entities;
namespace HotelBookingKata.Repositories
{
    public interface BookingPolicyRepository
    {
        void SetCompanyPolicy(string companyId, List<RoomType> roomTypes);
        void SetEmployeePolicy(string employeeId, List<RoomType> roomTypes);
        bool HasCompanyPolicy(string companyId);
        bool HasEmployeePolicy(string employeeId);
        bool IsRoomTypeAlloedForCompany(string companyId, RoomType roomType);
        bool IsRoomTypeAlloedForEmployee(string employeeId, RoomType roomType);
      

    }
}
