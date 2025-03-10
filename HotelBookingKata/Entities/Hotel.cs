namespace HotelBookingKata.Entities;

public class Hotel {

    public string Id;
    public string Name;
    internal Dictionary<string, Room> Rooms = new Dictionary<string, Room>();



    public Hotel(string id, string name) {
        Id = id;
        Name = name;
    }

    public void SetRoom(string number, RoomType type)
    {
        Rooms[number] = new Room(number, type);
    }
}