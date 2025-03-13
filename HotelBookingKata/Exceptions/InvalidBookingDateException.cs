namespace HotelBookingKata.Exceptions
{
    public class InvalidBookingDateException : BookingException
    {
        public InvalidBookingDateException(string message) : base(message) { }
    }
}
