namespace HotelBookingKata.Exceptions
{
    public class EmployeeNotFoundException : CompanyException
    {
        private string EmployeeId { get; }

        public EmployeeNotFoundException(string employeeId) : base($"Employee with id {employeeId} not found")
        {
            EmployeeId = employeeId;
        }
    }
}
