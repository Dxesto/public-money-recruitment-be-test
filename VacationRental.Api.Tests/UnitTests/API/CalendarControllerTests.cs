using DTOs;
using Moq;
using System;
using VacationRental.Api.Tests.UnitTests.API.Fixtures;
using Xunit;

namespace VacationRental.Api.Tests.UnitTests.API
{
    public class CalendarControllerTests : IClassFixture<CalendarControllerFixture>, IDisposable
    {
        private readonly CalendarControllerFixture _callendarControllerFixture;

        public CalendarControllerTests(CalendarControllerFixture callendaControllerFixture)
        {
            _callendarControllerFixture = callendaControllerFixture;
        }

        [Fact]
        public void Get_WithValidData_ShouldReturnEntity()
        {
            // arrange and act
            var result = _callendarControllerFixture.Controller.Get(new CalendarDto());

            // assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Get_WithValidData_ShouldCallBl_Once()
        {
            // arrange and act
            var result = _callendarControllerFixture.Controller.Get(new CalendarDto());

            // assert
            _callendarControllerFixture.CalendaBlMock.Verify(x => x.Get(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<int>()), Times.Once);
        }

        public void Dispose()
        {
            _callendarControllerFixture.Clear();
        }
    }
}
