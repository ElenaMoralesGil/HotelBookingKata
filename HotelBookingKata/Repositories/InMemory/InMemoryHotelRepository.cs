using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;

namespace HotelBookingKata.Repositories.InMemory;

public class InMemoryHotelRepository : HotelRepository
{

    internal Dictionary<string, Hotel> hotels = new Dictionary<string, Hotel>();

    public void Add(Hotel hotel)
    {
        hotels[hotel.Id] = hotel;
    }

    public Hotel GetById(string id)
    {
        if (!hotels.ContainsKey(id))
        {
            throw new HotelNotFoundException(id);
        }
        return hotels[id];
    }

    public bool Exists(string id)
    {
        return hotels.ContainsKey(id);
    }

    public void Update(Hotel hotel)
    {
        hotels[hotel.Id] = hotel;
    }

    public bool HasRoomOfType(string hotelId, RoomType roomType)
    {
        return hotels[hotelId].GetRooms().Any(room => room.Type == roomType);
    }

    public int GetRoomsCount(string hotelId, RoomType roomType)
    {
        return hotels[hotelId].GetRooms().Count(room => room.Type == roomType);
    }
}