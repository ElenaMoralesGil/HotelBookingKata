using HotelBookingKata.Entities;
namespace HotelBookingKata.Exceptions;

public class RoomTypeNotAvailableException : BookingException
{
    public RoomType RoomType { get; }

    public RoomTypeNotAvailableException(RoomType roomType) : base($"Room type {roomType} is not available")
    {
        RoomType = roomType;
    }

}
