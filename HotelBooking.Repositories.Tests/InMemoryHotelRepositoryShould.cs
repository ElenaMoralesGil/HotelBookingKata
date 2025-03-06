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
    public void Test1()
    {
         var hotel =new Hotel("hotel1","hotel 1" );

         repository.Add(hotel);

         repository.hotels.Count.ShouldBe(1);
         repository.hotels.ShouldContainKey("hotel1");
    }
}
