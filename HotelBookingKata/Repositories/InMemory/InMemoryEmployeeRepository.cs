using HotelBookingKata.Entities;
namespace HotelBookingKata.Repositories.InMemory
{
    public class InMemoryEmployeeRepository : EmployeeRepository
    {

        private Dictionary<string, Employee> employees = new Dictionary<string, Employee>();

        public void Add(Employee employee)
        {

            employees.Add(employee.Id, employee);
        }

        public Employee? GetById(string id)
        {
            return employees.TryGetValue(id, out var employee) ? employee : null;
        }

        public bool Exists(string id)
        {
            return employees.ContainsKey(id);
        }

        public Dictionary<string, Employee> GetEmployees()
        {
            return employees;
        }

        public void Delete(string id)
        {
            employees.Remove(id);
        }

    }
}
