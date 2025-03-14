namespace HotelBookingKata.Exceptions
{
    public class EmployeeAlreadyExistsException : CompanyException
    {
        public string EmployeeId { get; }

        public EmployeeAlreadyExistsException(string employeeId) : base($"Employee with id {employeeId} already exists")
        {
            EmployeeId = employeeId;
        }
    }
}
