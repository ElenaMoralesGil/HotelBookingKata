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

    public Room GetRoom(string number)
    {
        if (!Rooms.ContainsKey(number))
        {
            throw new InvalidOperationException("Room not found");
        }
        return Rooms[number];
    }

    public List<Room> GetRooms()
    {
        return Rooms.Values.ToList();
    }
}