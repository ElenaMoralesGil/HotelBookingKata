using HotelBookingKata.Entities;

namespace HotelBookingKata.Repositories;

public interface HotelRepository{

    void Add(Hotel hotel);
    Hotel GetById(string id);
    bool Exists(string id);
    void Update(Hotel hotel);
}