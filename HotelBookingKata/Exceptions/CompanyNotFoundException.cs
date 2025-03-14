namespace HotelBookingKata.Exceptions
{
    public class CompanyNotFoundException : CompanyException
    {
        public string CompanyId { get; }

        public CompanyNotFoundException(string companyId) : base($"Company with id {companyId} not found")
        {
            CompanyId = companyId;
        }
    }
}
