using System.Net;
using HotelBookingKata.Entities;
using System.Net.Http.Json;
using Shouldly;


namespace HotelBookingKata.Test;

class BookingApiShould
{
    private CustomWebApplicationFactory<Program> factory;
    private HttpClient client;

    [SetUp]
    public void Setup()
    {
        factory = new CustomWebApplicationFactory<Program>();
        client = factory.CreateClient();

    }

    [TearDown]
    public void TearDown()
    {
        client?.Dispose();
        factory?.Dispose();
    }

    [Test]
    public void deny_booking_when_checkOut_date_is_before_checkIn_date()
    {
        var booking = new
        {
            EmployeeId = "Employee1",
            HotelId = "Hotel1",
            RoomType = RoomType.Standard,
            CheckIn = DateTime.Now.AddDays(1),
            CheckOut = DateTime.Now
        };
        var response = client.PostAsJsonAsync("/api/bookings", booking).Result;
        response.StatusCode.ShouldBe(HttpStatusCode.Conflict);
        var repository = factory.GetBookingRepository();
        repository.GetBookings().ShouldBeEmpty();
    }

    [Test]
    public void deny_booking_when_employee_does_not_exist()
    {
        var booking = new
        {
            EmployeeId = "Employee1",
            HotelId = "Hotel1",
            RoomType = RoomType.Standard,
            CheckIn = DateTime.Now,
            CheckOut = DateTime.Now.AddDays(1)
        };
        var response = client.PostAsJsonAsync("/api/bookings", booking).Result;
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        var repository = factory.GetBookingRepository();
        repository.GetBookings().ShouldBeEmpty();
    }

    [Test]
    public void deny_booking_when_hotel_does_not_exist()
    {
        var booking = new
        {
            EmployeeId = "Employee1",
            HotelId = "Hotel1",
            RoomType = RoomType.Standard,
            CheckIn = DateTime.Now,
            CheckOut = DateTime.Now.AddDays(1)
        };

        var response = client.PostAsJsonAsync("/api/bookings", booking).Result;

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        var repository = factory.GetBookingRepository();
        repository.GetBookings().ShouldBeEmpty();
    }

    [Test]
    public void deny_booking_when_hotel_does_not_have_the_room_type()
    {
        var company = new { id = "Company1", name = "Company 1" };
        client.PostAsJsonAsync("/api/companies", company).Wait();
        var hotel = new { id = "Hotel1", name = "Hotel 1" };
        client.PostAsJsonAsync("/api/hotels", hotel).Wait();
        var room = new { Number = "101", Type = RoomType.Standard };
        client.PutAsJsonAsync($"/api/hotels/{hotel.id}/rooms/{room.Type}", room).Wait();
        var employee = new { EmployeeId = "Employee1" };
        client.PostAsJsonAsync($"/api/companies/{company.id}/employees", employee).Wait();
        var booking = new
        {
            EmployeeId = employee.EmployeeId,
            HotelId = hotel.id,
            RoomType = RoomType.JuniorSuite,
            CheckIn = DateTime.Now,
            CheckOut = DateTime.Now.AddDays(1)
        };

        var response = client.PostAsJsonAsync("/api/bookings", booking).Result;

        response.StatusCode.ShouldBe(HttpStatusCode.Conflict);
        var repository = factory.GetBookingRepository();
        repository.GetBookings().ShouldBeEmpty();
    }

    [Test]
    public void deny_booking_when_booking_policy_does_not_allowed_the_room_type()
    {
        var company = new { id = "Company1", name = "Company 1" };
        client.PostAsJsonAsync("/api/companies", company).Wait();
        var hotel = new { id = "Hotel1", name = "Hotel 1" };
        client.PostAsJsonAsync("/api/hotels", hotel).Wait();
        var room = new { Number = "101", Type = RoomType.Standard };
        client.PutAsJsonAsync($"/api/hotels/{hotel.id}/rooms/{room.Type}", room).Wait();
        var employee = new { EmployeeId = "Employee1" };
        client.PostAsJsonAsync($"/api/companies/{company.id}/employees", employee).Wait();
        client.PutAsJsonAsync($"/api/booking-policies/employees/{employee.EmployeeId}",
            new { RoomType = new[] { RoomType.JuniorSuite } }).Wait();
        var booking = new
        {
            EmployeeId = employee.EmployeeId,
            HotelId = hotel.id,
            RoomType = RoomType.Standard,
            CheckIn = DateTime.Now,
            CheckOut = DateTime.Now.AddDays(1)
        };

        var response = client.PostAsJsonAsync("/api/bookings", booking).Result;

        response.StatusCode.ShouldBe(HttpStatusCode.Conflict);
        var repository = factory.GetBookingRepository();
        repository.GetBookings().ShouldBeEmpty();
    }

    [Test]
    public void deny_booking_if_there_is_no_room_available_between_those_dates()
    {
        var company = new { id = "Company1", name = "Company 1" };
        client.PostAsJsonAsync("/api/companies", company).Wait();
        var hotel = new { id = "Hotel1", name = "Hotel 1" };
        client.PostAsJsonAsync("/api/hotels", hotel).Wait();
        var employee = new { EmployeeId = "Employee1" };
        client.PostAsJsonAsync($"/api/companies/{company.id}/employees", employee).Wait();
        client.PutAsJsonAsync($"/api/booking-policies/employees/{employee.EmployeeId}",
            new { RoomType = new[] { RoomType.Standard } }).Wait();
        var booking = new
        {
            EmployeeId = employee.EmployeeId,
            HotelId = hotel.id,
            RoomType = RoomType.Standard,
            CheckIn = DateTime.Now,
            CheckOut = DateTime.Now.AddDays(1)
        };
        
        var response = client.PostAsJsonAsync("/api/bookings", booking).Result;

        response.StatusCode.ShouldBe(HttpStatusCode.Conflict);
        var repository = factory.GetBookingRepository();
        repository.GetBookings().ShouldBeEmpty();
    }

    [Test]
    public async Task create_booking_for_same_room_in_different_date_periods()
    {
        var company = new { id = "Company1", name = "Company 1" };
        await client.PostAsJsonAsync("/api/companies", company);
        var hotel = new { id = "Hotel1", name = "Hotel 1" };
        await client.PostAsJsonAsync("/api/hotels", hotel);
        var room = new { Number = "101", Type = RoomType.Standard };
        await client.PutAsJsonAsync($"/api/hotels/{hotel.id}/rooms/{room.Type}", room);
        var employee = new { EmployeeId = "Employee1" };
        await client.PostAsJsonAsync($"/api/companies/{company.id}/employees", employee);
        await client.PutAsJsonAsync($"/api/booking-policies/employees/{employee.EmployeeId}",
            new { RoomType = new[] { RoomType.Standard } });
       
        var booking = new
        {
            EmployeeId = employee.EmployeeId,
            HotelId = hotel.id,
            RoomType = RoomType.Standard,
            CheckIn = DateTime.Now,
            CheckOut = DateTime.Now.AddDays(1)
        };
        var response = await client.PostAsJsonAsync("/api/bookings", booking);
        var bookingResponse = await response.Content.ReadFromJsonAsync<Booking>();
        var booking2 = new
        {
            EmployeeId = employee.EmployeeId,
            HotelId = hotel.id,
            RoomType = RoomType.Standard,
            CheckIn = DateTime.Now.AddDays(2),
            CheckOut = DateTime.Now.AddDays(3)
        };
        var response2 = await client.PostAsJsonAsync("/api/bookings", booking2);
        var bookingResponse2 = await response2.Content.ReadFromJsonAsync<Booking>();

        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        bookingResponse.EmployeeId.ShouldBe(booking.EmployeeId);
        bookingResponse.HotelId.ShouldBe(booking.HotelId);
        bookingResponse.RoomType.ShouldBe(booking.RoomType);
        bookingResponse.CheckIn.ShouldBe(booking.CheckIn);
        bookingResponse.CheckOut.ShouldBe(booking.CheckOut);
        response2.StatusCode.ShouldBe(HttpStatusCode.Created);
        bookingResponse2.EmployeeId.ShouldBe(booking2.EmployeeId);
        bookingResponse2.HotelId.ShouldBe(booking2.HotelId);
        bookingResponse2.RoomType.ShouldBe(booking2.RoomType);
        bookingResponse2.CheckIn.ShouldBe(booking2.CheckIn);
        bookingResponse2.CheckOut.ShouldBe(booking2.CheckOut);
        var repository = factory.GetBookingRepository();
        var bookings = repository.GetBookings();
        bookings.ShouldContainKey(bookingResponse.Id);
        bookings.ShouldContainKey(bookingResponse2.Id);

    }


    [Test]
    public async Task create_booking_when_all_conditions_are_met()
    {
        var company = new { id = "Company1", name = "Company 1" };
        await client.PostAsJsonAsync("/api/companies", company);

        var hotel = new { id = "Hotel1", name = "Hotel 1" };
        await client.PostAsJsonAsync("/api/hotels", hotel);

        var room = new { Number = "101", Type = RoomType.Standard };
        await client.PutAsJsonAsync($"/api/hotels/{hotel.id}/rooms/{room.Type}", room);

        var employee = new { EmployeeId = "Employee1" };
        await client.PostAsJsonAsync($"/api/companies/{company.id}/employees", employee);
        await client.PutAsJsonAsync($"/api/booking-policies/employees/{employee.EmployeeId}",
            new { RoomType = new[] { RoomType.Standard } });

        var booking = new
        {
            EmployeeId = employee.EmployeeId,
            HotelId = hotel.id,
            RoomType = RoomType.Standard,
            CheckIn = DateTime.Now,
            CheckOut = DateTime.Now.AddDays(1)
        };

        var response = await client.PostAsJsonAsync("/api/bookings", booking);

        response.StatusCode.ShouldBe(HttpStatusCode.Created);

        var bookingResponse = await response.Content.ReadFromJsonAsync<Booking>();
        bookingResponse.ShouldNotBeNull();
        bookingResponse.EmployeeId.ShouldBe(booking.EmployeeId);
        bookingResponse.HotelId.ShouldBe(booking.HotelId);
        bookingResponse.RoomType.ShouldBe(booking.RoomType);
        bookingResponse.CheckIn.ShouldBe(booking.CheckIn);
        bookingResponse.CheckOut.ShouldBe(booking.CheckOut);

        var repository =  factory.GetBookingRepository();
        var bookings = repository.GetBookings();
        bookings.ShouldContainKey(bookingResponse.Id);

    }


}

