using HotelBookingKata.Exceptions;

namespace HotelBookingKata.Entities;

public class Hotel
{

    public string Id { get; private set; }
    public string Name { get; private set; }
    internal List<Room> Rooms = new List<Room>();



    public Hotel(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public void SetRoom(string number, RoomType type)
    {
        Rooms.Add(new Room(type, number));
    }

    public Room GetRoom(RoomType type)
    {
        var room = Rooms.Find(room => room.Type == type);
        if (room == null) throw new RoomNotFoundException(Id, type);

        return room;
    }

    public List<Room> GetRooms()
    {
        return Rooms;
    }
}