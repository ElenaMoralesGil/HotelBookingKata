namespace HotelBookingKata.Exceptions
{
    public class InvalidBookingDatesException : BookingException
    {
        public InvalidBookingDatesException(string message) : base(message) { }
    }
}
