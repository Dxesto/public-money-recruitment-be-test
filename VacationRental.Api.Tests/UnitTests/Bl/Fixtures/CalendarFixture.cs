using BL;
using BL.Interfaces;

namespace VacationRental.Api.Tests.UnitTests.Bl.Fixtures
{
    public class CalendarFixture : BaseFixture
    {
        public ICalendarBl CalendarBl { get; private set; }

        public CalendarFixture()
        {
            CalendarBl = new CalendarBl(UnitOfWork);
        }
    }
}
