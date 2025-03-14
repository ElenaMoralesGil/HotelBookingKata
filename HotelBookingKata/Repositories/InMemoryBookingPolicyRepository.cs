using HotelBookingKata.Entities;

namespace HotelBookingKata.Repositories;
public class InMemoryBookingPolicyRepository : BookingPolicyRepository
{

    private Dictionary<string, CompanyBookingPolicy> companyPolicies = new Dictionary<string, CompanyBookingPolicy>();
    private Dictionary<string, EmployeeBookingPolicy> employeePolicies = new Dictionary<string, EmployeeBookingPolicy>();


    public void SetCompanyPolicy(string companyId, List<RoomType> roomTypes)
    {
        companyPolicies[companyId] = new CompanyBookingPolicy(companyId, roomTypes);
    }

    public void SetEmployeePolicy(string employeeId, List<RoomType> roomTypes)
    {
        employeePolicies[employeeId] = new EmployeeBookingPolicy(employeeId, roomTypes);
    }

    public bool HasCompanyPolicy(string companyId)
    {
        return companyPolicies.ContainsKey(companyId);
    }

    public bool HasEmployeePolicy(string employeeId)
    {
        return employeePolicies.ContainsKey(employeeId);
    }

    public bool IsRoomTypeAllowedForCompany(string companyId, RoomType roomType)
    {
        if (HasCompanyPolicy(companyId) is false) return false;

        return companyPolicies[companyId].IsRoomTypeAllowed(roomType);
    }

    public bool IsRoomTypeAllowedForEmployee(string employeeId, RoomType roomType)
    {
        if (HasEmployeePolicy(employeeId) is false) return false;

        return employeePolicies[employeeId].IsRoomTypeAllowed(roomType);
    }

    public void DeleteEmployeePolicy(string employeeId)
    {
        employeePolicies.Remove(employeeId);
    }

    public Dictionary<string, EmployeeBookingPolicy> GetEmployeePolicies()
    {
        return employeePolicies;
    }
}

