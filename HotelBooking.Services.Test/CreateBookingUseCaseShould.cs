using HotelBookingKata.Entities;
using HotelBookingKata.Repositories;
using NSubstitute;
using Shouldly;
using HotelBookingKata.CheckBookingPolicy;
using HotelBookingKata.CreateBooking;
namespace HotelBooking.Services.Test;

class CreateBookingPolicyUseCaseShould
{
    private BookingRepository bookingRepository;
    private HotelRepository hotelRepository;
    private CheckBookingPolicyRepository checkBookingPolicyRepository;
    private CreateBookingUseCase bookingUseCase;

    [SetUp]
    public void Setup()
    {
        bookingRepository = Substitute.For<BookingRepository>();
        hotelRepository = Substitute.For<HotelRepository>();
        checkBookingPolicyRepository= Substitute.For<CheckBookingPolicyRepository>();
        bookingUseCase = new CreateBookingUseCase(hotelRepository, bookingRepository, checkBookingPolicyRepository);
    }

    [Test]
    public void book_when_booking_is_valid()
    {
        var employeeId = "Employee1";
        var hotelId = "Hotel1";
        var roomType = RoomType.Standard;
        var checkIn = DateTime.Now;
        var checkOut = DateTime.Now.AddDays(1);
        var hotel = new Hotel(hotelId, "Hotel 1");
        hotel.SetRoom("101", roomType);
        hotelRepository.GetById(hotelId).Returns(hotel);
        hotelRepository.Exists(hotelId).Returns(true);
        hotelRepository.HasRoomOfType(hotelId, roomType).Returns(true);
        hotelRepository.GetRoomsCount(hotelId, roomType).Returns(1);
        checkBookingPolicyRepository.IsBookingAllowed(employeeId, roomType).Returns(true);
        bookingRepository.CountBookingsByHotelRoomType(hotelId, roomType, Arg.Any<DateTime>()).Returns(0);
        var request = new CreateBookingRequest(employeeId, hotelId, roomType, checkIn, checkOut);
        var booking = bookingUseCase.Execute(request);

        booking.ShouldNotBeNull();
        booking.EmployeeId.ShouldBe(employeeId);
        booking.HotelId.ShouldBe(hotelId);
        booking.RoomType.ShouldBe(roomType);
        booking.CheckIn.ShouldBe(checkIn);
        booking.CheckOut.ShouldBe(checkOut);
        bookingRepository.Received(1).Add(Arg.Is<Booking>(b =>
            b.EmployeeId == employeeId &&
            b.HotelId == hotelId &&
            b.RoomType == roomType &&
            b.CheckIn == checkIn &&
            b.CheckOut == checkOut));
    }

}

