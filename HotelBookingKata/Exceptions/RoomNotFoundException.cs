using HotelBookingKata.Entities;

namespace HotelBookingKata.Exceptions
{
    public class RoomNotFoundException :  HotelException
    {
        public RoomType RoomType { get; }
        public string HotelId { get; }

        public RoomNotFoundException(string hotelId, RoomType roomType) : base($"Room with type {roomType} not found in hotel with id {hotelId}")
        {
            RoomType = roomType;
            HotelId = hotelId;
        }
    }
}
