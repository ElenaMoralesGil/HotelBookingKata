using HotelBookingKata.Entities;
namespace HotelBookingKata.Repositories
{
    public interface EmployeeRepository
    {
        void Add(Employee employee);
        Employee? GetById(string id);
        bool Exists(string id);
        Dictionary<string, Employee> GetEmployees();

    }
}
