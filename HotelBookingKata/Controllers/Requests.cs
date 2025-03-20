using HotelBookingKata.Entities;

namespace HotelBookingKata.Controllers;

public record SetEmployeePolicyRequest(List<RoomType> RoomType);
public record AddEmployeeRequest(string EmployeeId);
public record SetRoomNumberRequest(string Number);