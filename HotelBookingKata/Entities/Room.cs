namespace HotelBookingKata.Entities
{
    public class Room
    {
        public string Number { get; }
        public RoomType Type { get; }
        public Room(RoomType type, string number)
        {
            Number = number;
            Type = type;
        }
    }

    public enum RoomType
    {
        Standard,
        JuniorSuite,
        MasterSuite
    }
}
