namespace HotelBookingKata.Entities;
public class BookingPolicy
{
    public List<RoomType> AllowedRoomTypes { get; private set; }

    public BookingPolicy(List<RoomType> allowedRoomTypes)
    {
        AllowedRoomTypes = allowedRoomTypes;
    }

    public bool IsRoomTypeAllowed(RoomType roomType)
    {
        return AllowedRoomTypes.Contains(roomType);
    }
}

public class CompanyBookingPolicy : BookingPolicy
{
    public string CompanyId { get; private set; }
    public CompanyBookingPolicy(string companyId, List<RoomType> allowedRoomTypes) : base(allowedRoomTypes)
    {
        CompanyId = companyId;
    }
}

public class EmployeeBookingPolicy : BookingPolicy
{
    public string EmployeeId { get; private set; }
    public EmployeeBookingPolicy(string employeeId, List<RoomType> allowedRoomTypes) : base(allowedRoomTypes)
    {
        EmployeeId = employeeId;
    }
}

