namespace HotelBookingKata;

public interface HotelRepository{

    void Add(Hotel hotel);
    Hotel GetById(string id);
}