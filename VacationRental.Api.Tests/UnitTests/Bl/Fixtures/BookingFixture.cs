using BL;
using BL.Interfaces;

namespace VacationRental.Api.Tests.UnitTests.Bl.Fixtures
{
    public class BookingFixture : BaseFixture
    {
        public IBookingBl BookingBl { get; private set; }

        public BookingFixture()
        {
            BookingBl = new BookingBl(UnitOfWork);
        }
    }
}
