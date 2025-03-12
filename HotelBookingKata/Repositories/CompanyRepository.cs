using HotelBookingKata.Entities;
namespace HotelBookingKata.Repositories
{
    public interface CompanyRepository
    {
        void Add(Company company);
        Company? GetById(string id);
        bool Exists(string id);
        void Update(Company company);
        Dictionary<string, Company> GetCompanies();


    }
}
