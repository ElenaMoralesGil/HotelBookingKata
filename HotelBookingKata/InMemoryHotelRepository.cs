namespace HotelBookingKata;

public class InMemoryHotelRepository : HotelRepository {

    internal Dictionary<string, Hotel> hotels = new Dictionary<string, Hotel>();

    public void Add(Hotel hotel) {
        hotels[hotel.Id] = hotel;
    }
    public Hotel GetById(string id) {
        return hotels[id];
    }

    public bool Exists(string id) {
        return hotels.ContainsKey(id);
    }
}