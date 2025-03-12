namespace HotelBookingKata.Entities
{
    public class Company
    {
        public string Id { get; private set; }
        private List<Employee> employees = new List<Employee>();

        public Company(string id) {
            Id = id;
        }

        public void AddEmployee(Employee employee)
        {
            employees.Add(employee);
        }

    }
}
