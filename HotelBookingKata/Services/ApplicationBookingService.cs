using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Repositories;
namespace HotelBookingKata.Services;

public class ApplicationBookingService :  BookingService
{
    private BookingRepository bookingRepository;
    private HotelRepository hotelRepository;
    private BookingPolicyService bookingPolicyService;

    public ApplicationBookingService(BookingRepository bookingRepository, HotelRepository hotelRepository, BookingPolicyService bookingPolicyService)
    {
        this.bookingRepository = bookingRepository;
        this.hotelRepository = hotelRepository;
        this.bookingPolicyService = bookingPolicyService;
    }

    public Booking Book(string employeeId, string hotelId, RoomType roomType, DateTime checkIn, DateTime checkOut)
    {
        if(checkOut <= checkIn)
        {
            throw new InvalidBookingDateException("Checkout date must be after Checkin date");
        }

        if (!hotelRepository.Exists(hotelId))
        {
            throw new HotelNotFoundException(hotelId);
        }

        var hotel = hotelRepository.GetById(hotelId);

        if (!hotelRepository.HasRoomOfType(hotelId, roomType))
        {
            throw new RoomTypeNotAvailableException(roomType);
        }

        if (!bookingPolicyService.IsBookingAllowed(employeeId, roomType))
        {
            throw new BookingNotAllowedException(employeeId, roomType);
        }

        var roomsCount = hotelRepository.GetRoomsCount(hotelId, roomType);

        for ( var date = checkIn; date < checkOut; date = date.AddDays(1))
        {
            var bookingsCount = bookingRepository.CountBookingsByHotelRoomType(hotelId, roomType, date);
            if (bookingsCount >= roomsCount)
            {
                throw new NoRoomsAvailableException(hotelId, roomType, checkIn,checkOut);
            }
        }

        var bookingId = Guid.NewGuid().ToString();
        var booking = new Booking(bookingId, employeeId, hotelId, roomType, checkIn, checkOut);
        bookingRepository.Add(booking);

        return booking;

    }
}
