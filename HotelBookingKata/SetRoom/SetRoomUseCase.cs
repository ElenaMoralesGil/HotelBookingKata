using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Repositories;

namespace HotelBookingKata.SetRoom
{
    public class SetRoomUseCase
    {
        private HotelRepository HotelRepository;

        public SetRoomUseCase(HotelRepository hotelRepository)
        {
            HotelRepository = hotelRepository;
        }
        public SetRoomUseCase()
        {
        }
        public virtual void Execute(string hotelId, string roomNumber, RoomType roomType)
        {
            if (!HotelRepository.Exists(hotelId)) throw new HotelNotFoundException(hotelId);

            var hotel = HotelRepository.GetById(hotelId);
            hotel.SetRoom(roomNumber, roomType);
            HotelRepository.Update(hotel);
        }
    }
}
