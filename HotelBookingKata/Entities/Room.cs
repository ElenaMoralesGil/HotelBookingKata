namespace HotelBookingKata.Entities
{
    public class Room
    {
        public string Number;
        public RoomType Type;
        public Room( RoomType type, string number)
        {
            Number = number;
            Type = type;
        }
    }

    public enum RoomType
    {
        Standard,
        JuniorSuite
    }
}
