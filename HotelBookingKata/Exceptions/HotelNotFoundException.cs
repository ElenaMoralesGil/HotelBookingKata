namespace HotelBookingKata.Exceptions
{
    public class HotelNotFoundException : HotelException
    {
        public string HotelId { get; }
        public HotelNotFoundException(string hotelId) : base($"Hotel with id {hotelId} not found")
        {
            HotelId = hotelId;
        }
    }
}
