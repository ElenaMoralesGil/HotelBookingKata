namespace HotelBookingKata.Entities
{
    public class Room
    {
        public string Number;
        public RoomType Type;
        public Room(string number, RoomType type)
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
