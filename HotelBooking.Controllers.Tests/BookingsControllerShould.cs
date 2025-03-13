using HotelBookingKata;
using HotelBookingKata.Services;
using HotelBookingKata.Controllers;
using HotelBookingKata.Entities;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;

namespace HotelBooking.Controllers.Tests;

class BookingsControllerShould
{
    private BookingsController controller;
    private BookingService bookingService;

    [SetUp]
    public void Setup()
    {
        bookingService = Substitute.For<BookingService>();
        controller = new BookingsController(bookingService);
    }

    [Test]
    public void return_created_when_booking_is_succesful()
    {
        var employeeId = "Employee1";
        var hotelId = "Hotel1";
        var roomType = RoomType.Standard;
        var checkIn = DateTime.Now;
        var checkOut = DateTime.Now.AddDays(1);
        var booking = new Booking("Booking1", employeeId, hotelId, roomType, checkIn, checkOut);
        var request = new CreateBookingRequest(employeeId, hotelId, roomType, checkIn, checkOut);
        bookingService.Book(employeeId, hotelId, roomType, checkIn, checkOut).Returns(booking);

        var result = controller.CreateBooking(request);

        result.ShouldBeOfType<CreatedResult>();
        var createdResult = (CreatedResult)result;
        createdResult.Location.ShouldContain(booking.Id);

    }
}
