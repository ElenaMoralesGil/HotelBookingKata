using HotelBookingKata.Entities;
using HotelBookingKata.Repositories.InMemory;
using Shouldly;

namespace HotelBooking.Repositories.Tests;

class InMemoryBookingRepositoryShould
{
    private InMemoryBookingRepository repository;

    [SetUp]
    public void Setup()
    {
        repository = new InMemoryBookingRepository();
    }

    [Test]
    public void add_booking_to_existing_bookings()
    {
        var booking = new Booking("Booking1", "Employee1", "Hotel1", RoomType.Standard, DateTime.Now, DateTime.Now.AddDays(1));

        repository.Add(booking);

        repository.Exists(booking.Id).ShouldBeTrue();
    }

    [Test]
    public void delete_booking_from_existing_employee()
    {
        var booking = new Booking("Booking1", "Employee1", "Hotel1", RoomType.Standard, DateTime.Now, DateTime.Now.AddDays(1));
        repository.Add(booking);
        repository.DeleteEmployeeBookings("Employee1");
        repository.Exists(booking.Id).ShouldBeFalse();
    }
}
