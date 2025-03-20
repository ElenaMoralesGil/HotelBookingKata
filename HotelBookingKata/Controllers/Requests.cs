using HotelBookingKata.Entities;

namespace HotelBookingKata.Controllers;

public record SetCompanyPolicyRequest(List<RoomType> RoomType);
public record SetEmployeePolicyRequest(List<RoomType> RoomType);
public record AddEmployeeRequest(string EmployeeId);
public record SetRoomNumberRequest(string Number);