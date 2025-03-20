using HotelBookingKata.Entities;

namespace HotelBookingKata.CreateBooking;

public record CreateBookingRequest(string EmployeeId, string HotelId, RoomType RoomType, DateTime CheckIn, DateTime CheckOut);
