using HotelBookingKata.Entities;

namespace HotelBookingKata.Controllers;

public record HotelResponse(string Id, string Name, List<RoomResponse> Rooms);
public record RoomResponse(string Number, RoomType Type);
