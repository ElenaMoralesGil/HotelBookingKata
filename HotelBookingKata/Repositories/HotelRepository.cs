using HotelBookingKata.Entities;

namespace HotelBookingKata.Repositories;

public interface HotelRepository{

    void Add(Hotel hotel);
    Hotel GetById(string id);
    bool Exists(string id);
    void Update(Hotel hotel);

    bool HasRoomOfType(string hotelId, RoomType roomType);
    int GetRoomsCount(string hotelId, RoomType roomType);
}