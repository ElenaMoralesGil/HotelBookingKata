using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Repositories;

namespace HotelBookingKata.AddHotel
{
    public class AddHotelUseCase
    {

        private HotelRepository HotelRepository;

        public AddHotelUseCase() { }

        public AddHotelUseCase(HotelRepository hotelRepository)
        {
            HotelRepository = hotelRepository;
        }

        public virtual void Execute(AddHotelRequest request)
        {

            if (HotelRepository.Exists(request.Id)) throw new HotelAlreadyExistsException(request.Id);

            var hotel = new Hotel(request.Id, request.Name);
            HotelRepository.Add(hotel);
        }
    }
}
