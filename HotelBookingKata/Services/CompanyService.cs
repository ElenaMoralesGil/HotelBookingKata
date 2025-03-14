namespace HotelBookingKata.Services
{
    public interface CompanyService
    {
        void AddEmployee(string companyId, string employeeId);
        void DeleteEmployee(string employeeId);
    }
}
