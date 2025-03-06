namespace HotelBookingKata;

public interface HotelRepository{

    void Add(Hotel hotel);
    Hotel GetById(string id);
    bool Exists(string id);
}