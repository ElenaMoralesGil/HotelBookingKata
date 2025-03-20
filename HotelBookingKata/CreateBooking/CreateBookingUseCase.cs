using HotelBookingKata.Controllers;
using HotelBookingKata.Repositories;
using HotelBookingKata.Entities;
using HotelBookingKata.Adapters;
using HotelBookingKata.Exceptions;
namespace HotelBookingKata.CreateBooking;

public class CreateBookingUseCase
{
    private HotelRepository hotelRepository;
    private BookingRepository bookingRepository;
    private CheckBookingPermissionRepository CheckBookingPermissionRepository;

    public CreateBookingUseCase() { }
    public CreateBookingUseCase(HotelRepository hotelRepository, BookingRepository bookingRepository, CheckBookingPermissionRepository CheckBookingPermissionRepository)
    {
        this.hotelRepository = hotelRepository;
        this.bookingRepository = bookingRepository;
        this.CheckBookingPermissionRepository = CheckBookingPermissionRepository;
    }

    public virtual Booking Execute(CreateBookingRequest request)
    {
       ValidateBooking(request);
       return CreateBooking(request.EmployeeId, request.HotelId, request.RoomType, request.CheckIn, request.CheckOut);
    }

    private void ValidateBooking(CreateBookingRequest request)
    {
        ValidateBookingDates(request.CheckIn, request.CheckOut);
        ValidateHotelExists(request.HotelId);
        ValidateIfHotelHasRoomType(request.HotelId, request.RoomType);
        ValidateIfBookingIsAllowed(request.EmployeeId, request.RoomType);
        ValidateIfRoomIsAvailable(request.HotelId, request.RoomType, request.CheckIn, request.CheckOut);
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
        bool isAllowed = await CheckBookingPermissionRepository.IsBookingAllowed(employeeId, roomType);
        if (isAllowed is false)
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
