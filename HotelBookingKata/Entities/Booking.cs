namespace HotelBookingKata.Entities
{
    public class Booking
    {
        public string Id { get; private set; }
        public string EmployeeId { get; private set; }
        public string HotelId { get; private set; }
        public RoomType RoomType { get; private set; }
        public DateTime CheckIn { get; private set; }
        public DateTime CheckOut { get; private set; }

        public Booking(string id, string employeeId, string hotelId, RoomType roomType, DateTime checkIn, DateTime checkOut)
        {
            Id = id;
            EmployeeId = employeeId;
            HotelId = hotelId;
            RoomType = roomType;
            CheckIn = checkIn;
            CheckOut = checkOut;
        }
    }
}
