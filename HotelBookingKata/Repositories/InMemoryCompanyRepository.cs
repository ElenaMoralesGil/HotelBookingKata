using HotelBookingKata.Entities;
namespace HotelBookingKata.Repositories
{
    public class InMemoryCompanyRepository :  CompanyRepository
    {
        private Dictionary<string, Company> companies = new Dictionary<string, Company>();
        public void Add(Company company)
        {
            companies.Add(company.Id, company);
        }
        public Company? GetById(string id)
        {
            return companies.TryGetValue(id , out var company) ? company : null;
        }
        public bool Exists(string id)
        {
            return companies.ContainsKey(id);
        }

        public void Update(Company company)
        {
            companies[company.Id] = company;
        }

        public Dictionary<string, Company> GetCompanies()
        {
            return companies;
        }

    }
}
