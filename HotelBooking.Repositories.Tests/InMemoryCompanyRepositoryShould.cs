using System.Net;
using HotelBookingKata.Repositories;
using HotelBookingKata.Entities;
using NSubstitute;
using Shouldly;
using HotelBookingKata.Exceptions;
namespace HotelBooking.Repositories.Tests;
class InMemoryCompanyRepositoryShould
{

    private InMemoryCompanyRepository repository;

    [SetUp]
    public void Setup()
    {
        repository = new InMemoryCompanyRepository();
    }

    [Test]
    public void add_company_to_existing_companies()
    {
        var company = new Company("company1");

        repository.Add(company);
        var exists = repository.Exists(company.Id);
        exists.ShouldBeTrue();
        var companies = repository.GetCompanies();
        companies.Count.ShouldBe(1);
        companies.ShouldContainKey("company1");
    }

    [Test]
    public void get_id_from_existing_company()
    {
        var company = new Company("company1");
        repository.Add(company);

        var result = repository.GetById(company.Id);

        result.ShouldBe(company);
    }

    

    [Test]
    public void return_true_when_company_exists()
    {
        var company = new Company("company1");
        repository.Add(company);

        var result = repository.Exists(company.Id);

        result.ShouldBeTrue();
    }

    [Test]
    public void return_false_when_company_doesnt_exist()
    {
        var company = new Company("company1");
        var result = repository.Exists(company.Id);

        result.ShouldBeFalse();
    }
}

