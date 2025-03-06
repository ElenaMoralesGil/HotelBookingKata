namespace HotelBookingKata;

public class HotelService {

    private readonly HotelRepository HotelRepository;

    public HotelService(HotelRepository hotelRepository){
        HotelRepository= hotelRepository;
    }

    public void AddHotel(string hotelId, string hotelName){
        var hotel =  new Hotel(hotelId,hotelName);
        HotelRepository.Add(hotel);
    }
}