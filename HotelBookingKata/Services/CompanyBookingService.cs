using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Repositories;
namespace HotelBookingKata.Services;

public class CompanyBookingService : BookingService
{
    private BookingRepository bookingRepository;
    private HotelRepository hotelRepository;
    private BookingPolicyService bookingPolicyService;

    public CompanyBookingService(BookingRepository bookingRepository, HotelRepository hotelRepository, BookingPolicyService bookingPolicyService)
    {
        this.bookingRepository = bookingRepository;
        this.hotelRepository = hotelRepository;
        this.bookingPolicyService = bookingPolicyService;
    }

    public Booking Book(string employeeId, string hotelId, RoomType roomType, DateTime checkIn, DateTime checkOut)
    {
    
        ValidateBookingDates(checkIn, checkOut);
        ValidateHotelExists(hotelId);
        ValidateIfHotelHasRoomType(hotelId, roomType);
        ValidateIfBookingIsAllowed(employeeId, roomType);
        ValidateIfRoomIsAvailable(hotelId, roomType, checkIn, checkOut);

        return CreateBooking(employeeId, hotelId, roomType, checkIn, checkOut);

    }

    private void ValidateBookingDates(DateTime checkIn, DateTime checkOut)
    {
        if (checkOut <= checkIn)
        {
            throw new InvalidBookingDateException("Checkout date must be after Checkin date");
        }
    }

    private void ValidateHotelExists(string hotelId)
    {
        if (!hotelRepository.Exists(hotelId))
        {
            throw new HotelNotFoundException(hotelId);
        }
    }

    private void ValidateIfHotelHasRoomType(string hotelId, RoomType roomType)
    {
        if (!hotelRepository.HasRoomOfType(hotelId, roomType))
        {
            throw new RoomTypeNotAvailableException(roomType);
        }
    }

    private void ValidateIfBookingIsAllowed(string employeeId, RoomType roomType)
    {
        if (!bookingPolicyService.IsBookingAllowed(employeeId, roomType))
        {
            throw new BookingNotAllowedException(employeeId, roomType);
        }
    }

    private void ValidateIfRoomIsAvailable(string hotelId, RoomType roomType, DateTime checkIn, DateTime checkOut)
    {
        var roomsCount = hotelRepository.GetRoomsCount(hotelId, roomType);
        for (var date = checkIn; date < checkOut; date = date.AddDays(1))
        {
            var bookingsCount = bookingRepository.CountBookingsByHotelRoomType(hotelId, roomType, date);
            if (bookingsCount >= roomsCount)
            {
                throw new NoRoomsAvailableException(hotelId, roomType, checkIn, checkOut);
            }
        }
    }

    private Booking CreateBooking(string employeeId, string hotelId, RoomType roomType, DateTime checkIn, DateTime checkOut)
    {
        var bookingId = Guid.NewGuid().ToString();
        var booking = new Booking(bookingId, employeeId, hotelId, roomType, checkIn, checkOut);
        bookingRepository.Add(booking);

        return booking;
    }
}
