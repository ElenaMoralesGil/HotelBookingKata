using NSubstitute;
using Shouldly;
using HotelBookingKata.Services;
using HotelBookingKata.Entities;
using HotelBookingKata.Repositories;
using HotelBookingKata.Exceptions;

namespace HotelBooking.Services.Test;
    class ApplicationBookingPolicyServiceShould
    {
        private BookingPolicyRepository bookingPolicyRepository;
        private EmployeeRepository employeeRepository;
        private CompanyRepository companyRepository;
        private ApplicationBookingPolicyService bookingPolicyService;

        [SetUp]
        public void Setup()
        {
            bookingPolicyRepository = Substitute.For<BookingPolicyRepository>();
            employeeRepository = Substitute.For<EmployeeRepository>();
            companyRepository = Substitute.For<CompanyRepository>();
            bookingPolicyService = new ApplicationBookingPolicyService(bookingPolicyRepository, employeeRepository, companyRepository);
        }

        [Test]
        public void set_company_policy_when_company_exists() {  

            var companyId = "Company1";
            var roomTypes = new List<RoomType> { RoomType.Standard};
            companyRepository.Exists(companyId).Returns(true);

            companyRepository.GetById(companyId).Returns(new Company(companyId));
            bookingPolicyService.SetCompanyPolicy(companyId, roomTypes);

            bookingPolicyRepository.Received(1).SetCompanyPolicy(companyId, roomTypes);
        }
}
