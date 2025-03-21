﻿using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.CreateBooking;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;

namespace HotelBooking.Controllers.Tests;

class CreateBookingControllerShould
{
    private CreateBookingController controller;
    private CreateBookingUseCase createBookingUseCase;

    [SetUp]
    public void Setup()
    {
        createBookingUseCase = Substitute.For<CreateBookingUseCase>();
        controller = new CreateBookingController(createBookingUseCase);
    }

    [Test]
    public void return_conflict_when_the_date_period_is_not_valid()
    {
        var employeeId = "Employee1";
        var hotelId = "Hotel1";
        var roomType = RoomType.Standard;
        var checkIn = DateTime.Now;
        var checkOut = DateTime.Now.AddDays(-1);
        var request = new CreateBookingRequest(employeeId, hotelId, roomType, checkIn, checkOut);
        createBookingUseCase.When(x => x.Execute(request)).Throw(new InvalidBookingDateException("Checkout date must be after Checkin date"));
        var result = controller.CreateBooking(request);
        result.ShouldBeOfType<ConflictObjectResult>();
    }

    [Test]
    public void return_conflict_when_there_is_no_room_available_of_a_type()
    {

        var employeeId = "Employee1";
        var hotelId = "Hotel1";
        var roomType = RoomType.Standard;
        var checkIn = DateTime.Now;
        var checkOut = DateTime.Now.AddDays(1);
        var request = new CreateBookingRequest(employeeId, hotelId, roomType, checkIn, checkOut);
        createBookingUseCase.When(x => x.Execute(request)).Throw(new RoomTypeNotAvailableException(roomType));
        var result = controller.CreateBooking(request);
        result.ShouldBeOfType<ConflictObjectResult>();
    }

    [Test]
    public void return_not_found_when_the_hotel_does_not_exist()
    {
        var employeeId = "Employee1";
        var hotelId = "Hotel1";
        var roomType = RoomType.Standard;
        var checkIn = DateTime.Now;
        var checkOut = DateTime.Now.AddDays(1);
        var request = new CreateBookingRequest(employeeId, hotelId, roomType, checkIn, checkOut);
        createBookingUseCase.When(x => x.Execute(request)).Throw(new HotelNotFoundException(hotelId));
        var result = controller.CreateBooking(request);
        result.ShouldBeOfType<NotFoundObjectResult>();
    }

    [Test]
    public void return_conflict_when_booking_is_not_allowrd_by_policy()
    {
        var employeeId = "Employee1";
        var hotelId = "Hotel1";
        var roomType = RoomType.Standard;
        var checkIn = DateTime.Now;
        var checkOut = DateTime.Now.AddDays(1);
        var request = new CreateBookingRequest(employeeId, hotelId, roomType, checkIn, checkOut);
        createBookingUseCase.When(x => x.Execute(request)).Throw(new BookingNotAllowedException(employeeId, roomType));
        var result = controller.CreateBooking(request);
        result.ShouldBeOfType<ConflictObjectResult>();
    }

    [Test]
    public void return_conflict_when_there_are_no_rooms_available_at_that_time_period()
    {
        var employeeId = "Employee1";
        var hotelId = "Hotel1";
        var roomType = RoomType.Standard;
        var checkIn = DateTime.Now;
        var checkOut = DateTime.Now.AddDays(1);
        var request = new CreateBookingRequest(employeeId, hotelId, roomType, checkIn, checkOut);
        createBookingUseCase.When(x => x.Execute(request)).Throw(new NoRoomsAvailableException(hotelId, roomType, checkIn, checkOut));
        var result = controller.CreateBooking(request);
        result.ShouldBeOfType<ConflictObjectResult>();
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
        createBookingUseCase.Execute(request).Returns(booking);

        var result = controller.CreateBooking(request);

        result.ShouldBeOfType<CreatedResult>();
        var createdResult = (CreatedResult)result;
        createdResult.Location?.ShouldContain(booking.Id);

    }
}
