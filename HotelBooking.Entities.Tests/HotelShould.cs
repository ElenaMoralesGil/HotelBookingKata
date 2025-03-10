using HotelBookingKata.Entities;
using Shouldly;
namespace HotelBooking.Entities.Tests;

public class HotelShould
{

    Hotel Hotel;
    [SetUp]
    public void Setup()
    {
        Hotel = new Hotel("1", "Hotel");
    }

    [Test]
    public void set_room()
    {
        Hotel.SetRoom("101", RoomType.Standard);
        Hotel.Rooms.Count.ShouldBe(1);
    }

    [Test]
    public void get_room()
    {
        Hotel.SetRoom("101", RoomType.Standard);
        var room = Hotel.GetRoom("101");
        room.Number.ShouldBe("101");
        room.Type.ShouldBe(RoomType.Standard);
    }

    [Test]
    public void get_rooms()
    {
        Hotel.SetRoom("101", RoomType.Standard);
        Hotel.SetRoom("102", RoomType.JuniorSuite);
        var rooms = Hotel.GetRooms();
        rooms.Count.ShouldBe(2);
    }
}
