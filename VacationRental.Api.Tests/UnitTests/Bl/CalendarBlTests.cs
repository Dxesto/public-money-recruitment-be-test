using Common;
using System;
using System.Linq;
using VacationRental.Api.Tests.UnitTests.Bl.Fixtures;
using Xunit;

namespace VacationRental.Api.Tests.UnitTests.Bl
{
    public class CalendarBlTests
    {
        private readonly CalendarFixture _fixture;

        public CalendarBlTests()
        {
            _fixture = new CalendarFixture();
        }

        [Fact]
        public void Get_WithInvalidRentalId_ShouldThrowApplicationException_WithRentalNotFoundMessagee()
        {
            // arrange
            int id = _fixture.GetRandomId();

            // act
            var ex = Assert.Throws<ApplicationException>(() =>
            {
                _fixture.CalendarBl.Get(id, default, default);
            });

            // assert
            Assert.Equal(ResponseMessages.EntityNotFound(ResourceTypes.Rental), ex.Message);
        }

        [Fact]
        public void Get_WithValidData_ShouldReturnCalendarWithBooking()
        {
            // arrange
            var booking =_fixture.CreateBooking();

            // act
            var calendar = _fixture.CalendarBl.Get(booking.RentalId, booking.Start, booking.Nights);

            // assert
            Assert.Equal(booking.Nights, calendar.Dates.SelectMany(x => x.Bookings).Count());
        }

        [Fact]
        public void Get_WithValidData_ShouldReturnCalendarWithPreparationTimes()
        {
            // arrange
            int unitPreparationTimeInDays = 2;
            var booking = _fixture.CreateBooking(unitPreparationTimeInDays: unitPreparationTimeInDays);

            // act
            var calendar = _fixture.CalendarBl.Get(booking.RentalId, booking.Start, booking.Nights);

            // assert
            Assert.Equal(unitPreparationTimeInDays, calendar.Dates.SelectMany(x => x.PreparationTimes).Count());
        }
    }
}
