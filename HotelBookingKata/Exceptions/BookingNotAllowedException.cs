using HotelBookingKata.Entities;
namespace HotelBookingKata.Exceptions
{
    public class BookingNotAllowedException : BookingException
    {
        public string EmployeeId { get; }
        public RoomType RoomType { get; }
        public BookingNotAllowedException(string employeeId, RoomType roomType) : base($"Booking not allowed for employee {employeeId} and room type {roomType}")
        {
            EmployeeId = employeeId;
            RoomType = roomType;
        }
    }
}
