namespace HotelBookingKata.Entities
{
    public class Employee
    {

        public string Id { get; private set; }
        public string CompanyId { get; private set; }

        public Employee(string id, string companyId)
        {
            Id = id;
            CompanyId = companyId;
        }
    }
}
