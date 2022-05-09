using BL.Interfaces;
using Models;
using Moq;
using System;
using VacationRental.Api.Controllers;

namespace VacationRental.Api.Tests.UnitTests.API.Fixtures
{
    public class CalendarControllerFixture
    {
        public readonly CalendarController Controller;

        public readonly Mock<ICalendarBl> CalendaBlMock;

        public CalendarControllerFixture()
        {
            CalendaBlMock = new Mock<ICalendarBl>();

            CalendaBlMock.Setup(x => x.Get(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<int>())).Returns(new Calendar());

            Controller = new CalendarController(CalendaBlMock.Object);
        }

        public void Clear()
        {
            CalendaBlMock.Invocations.Clear();
        }
    }
}
