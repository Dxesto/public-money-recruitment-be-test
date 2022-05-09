using BL;
using BL.Interfaces;

namespace VacationRental.Api.Tests.UnitTests.Bl.Fixtures
{
    public class RentalFixture : BaseFixture
    {
        public IRentalBl RentalBl { get; private set; }

        public RentalFixture()
        {
            RentalBl = new RentalBl(UnitOfWork);
        }
    }
}
