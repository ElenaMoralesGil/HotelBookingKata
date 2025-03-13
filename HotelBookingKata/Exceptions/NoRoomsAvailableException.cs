using HotelBookingKata.Entities;
namespace HotelBookingKata.Exceptions;

public class NoRoomsAvailableException : BookingException
{
    public string HotelId { get; }
    public RoomType RoomType { get; }
    public DateTime CheckIn { get; }
    public DateTime CheckOut { get; }

    public NoRoomsAvailableException(string hotelId, RoomType roomType, DateTime checkIn, DateTime checkOut)
        : base($"No rooms available for {roomType} at {hotelId} from {checkIn} to {checkOut}")
    {
        HotelId = hotelId;
        RoomType = roomType;
        CheckIn = checkIn;
        CheckOut = checkOut;
    }
}
