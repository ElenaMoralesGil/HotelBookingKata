namespace HotelBookingKata.Exceptions
{
    public class HotelAlreadyExistsException : HotelException
    {
        public string HotelId { get; }
        public HotelAlreadyExistsException(string hotelId) : base($"Hotel with id {hotelId} already exists")
        {
            HotelId = hotelId;
        }
    }
}
