using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Repositories;
using HotelBookingKata.Adapters;
using System.Threading.Tasks;
namespace HotelBookingKata.Services;

public class AppBookingService : BookingService
{
    private BookingRepository bookingRepository;
    private HotelRepository hotelRepository;
    private BookingPolicyAdapter bookingPolicyAdapter;

    public AppBookingService(BookingRepository bookingRepository, HotelRepository hotelRepository, BookingPolicyAdapter bookingPolicyAdapter)
    {
        this.bookingRepository = bookingRepository;
        this.hotelRepository = hotelRepository;
        this.bookingPolicyAdapter = bookingPolicyAdapter;
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

    private async Task ValidateIfBookingIsAllowed(string employeeId, RoomType roomType)
    {
        bool isAllowed =await   bookingPolicyAdapter.IsBookingAllowed(employeeId, roomType);
        if (isAllowed is false ) 
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
