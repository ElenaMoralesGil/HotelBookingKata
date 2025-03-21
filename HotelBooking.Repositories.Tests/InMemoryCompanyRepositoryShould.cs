
using HotelBookingKata.Entities;
using HotelBookingKata.Repositories.InMemory;
using Shouldly;
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

    [Test]
    public void update_existing_company()
    {
        var company = new Company("company1");
        repository.Add(company);
        var updatedCompany = new Company("company1");
        repository.Update(updatedCompany);
        var result = repository.GetById(company.Id);
    }

    [Test]
    public void get_companies()
    {
        var company1 = new Company("company1");
        var company2 = new Company("company2");
        repository.Add(company1);
        repository.Add(company2);
        var companies = repository.GetCompanies();
        companies.Count.ShouldBe(2);
        companies.ShouldContainKey("company1");
        companies.ShouldContainKey("company2");
    }
}

