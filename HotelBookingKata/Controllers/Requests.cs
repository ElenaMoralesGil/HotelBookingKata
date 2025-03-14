using HotelBookingKata.Entities;

namespace HotelBookingKata.Controllers;

public record SetCompanyPolicyRequest(List<RoomType> RoomType);
public record SetEmployeePolicyRequest(List<RoomType> RoomType);
public record CreateBookingRequest(string EmployeeId, string HotelId, RoomType RoomType, DateTime CheckIn, DateTime CheckOut);
public record AddEmployeeRequest(string EmployeeId);
public record SetRoomNumberRequest(string Number);
public record AddHotelRequest(string Id, string Name);