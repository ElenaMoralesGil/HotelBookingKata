namespace HotelBookingKata.Exceptions
{
    public class UseCaseNotFoundException :  Exception
    {
        public UseCaseNotFoundException() : base("Use case not found")
        {
        }
    }
}
