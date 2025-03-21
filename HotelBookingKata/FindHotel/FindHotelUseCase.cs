using HotelBookingKata.Entities;
using HotelBookingKata.Repositories;
namespace HotelBookingKata.Services;

public class FindHotelUseCase 
{

    private readonly HotelRepository HotelRepository;

    public FindHotelUseCase(HotelRepository hotelRepository)
    {
        HotelRepository = hotelRepository;
    }
    public FindHotelUseCase()
    {
    }

    public virtual Hotel Execute(string hotelId)
    {
        return HotelRepository.GetById(hotelId);
    }
}