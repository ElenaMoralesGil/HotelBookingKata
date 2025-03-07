namespace HotelBookingKata;

public class HotelService {

    private readonly HotelRepository HotelRepository;

    public HotelService(HotelRepository hotelRepository){
        HotelRepository= hotelRepository;
    }

    public void AddHotel(string hotelId, string hotelName){

        if (HotelRepository.Exists(hotelId)) throw new InvalidOperationException("Hotel already exists");
       
        var hotel =  new Hotel(hotelId,hotelName);
        HotelRepository.Add(hotel);
    }
}