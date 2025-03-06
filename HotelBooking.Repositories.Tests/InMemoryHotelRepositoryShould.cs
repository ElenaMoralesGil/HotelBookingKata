using System.Net;
using HotelBookingKata;
using NSubstitute;
using Shouldly;
namespace HotelBooking.Repositories.Tests;

public class InMemoryHotelRepositoryShould
{
    private InMemoryHotelRepository repository;

    [SetUp]
    public void Setup()
    {
        repository= new InMemoryHotelRepository();
    }

    [Test]
    public void add_hotel_to_hotels()
    {
         var hotel =new Hotel("hotel1","hotel 1" );

         repository.Add(hotel);

         repository.hotels.Count.ShouldBe(1);
         repository.hotels.ShouldContainKey("hotel1");
    }

    [Test]
    public void get_id_from_hotel_in_hotels(){
        var hotel =new Hotel("hotel1","hotel 1" );

        repository.Add(hotel);
        var result = repository.GetById(hotel.Id);

        result.ShouldBe(hotel);
    }
}
