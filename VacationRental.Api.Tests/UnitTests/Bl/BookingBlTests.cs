using Common;
using Models;
using System;
using VacationRental.Api.Tests.UnitTests.Bl.Fixtures;
using Xunit;

namespace VacationRental.Api.Tests.UnitTests.Bl
{
    public class BookingBlTests
    {
        private readonly BookingFixture _fixture;

        public BookingBlTests()
        {
            _fixture = new BookingFixture();
        }

        [Fact]
        public void GetById_WithInvalidId_ShouldThrowApplicationException_WithNotFoundMessagee()
        {
            // arrange
            int id = _fixture.GetRandomId();

            // act
            var ex = Assert.Throws<ApplicationException>(() =>
            {
                _fixture.BookingBl.GetById(id);
            });

            // assert
            Assert.Equal(ResponseMessages.EntityNotFound(ResourceTypes.Booking), ex.Message);
        }

        [Fact]
        public void GetById_WithValidId_ShouldReturnRental()
        {
            // arrange
            int id = _fixture.GetRandomId();
            _fixture.ApplicationContext.Set<Booking>().Add(id, new Booking());

            // act
            Booking booking = _fixture.BookingBl.GetById(id);

            // assert
            Assert.NotNull(booking);
        }

        [Fact]
        public void Create_WithInvalidRentalId_ShouldThrowApplicationException_WithNotFoundMessage()
        {
            // arrange
            int id = _fixture.GetRandomId();
            Booking booking = new Booking();

            // act
            var ex = Assert.Throws<ApplicationException>(() =>
            {
                _fixture.BookingBl.Create(booking);
            });

            // assert
            Assert.Equal(ResponseMessages.EntityNotFound(ResourceTypes.Rental), ex.Message);
        }

        [Fact]
        public void Create_WithOverlapedBooking_ShouldThrowApplicationException_WithNotAvailableMessage()
        {
            // arrange
            var booking = _fixture.CreateBooking();

            // act
            var ex = Assert.Throws<ApplicationException>(() =>
            {
                _fixture.BookingBl.Create(booking);
            });

            // assert
            Assert.Equal(ResponseMessages.NotAvailable, ex.Message);
        }

        [Fact]
        public void Create_WithOverlapedBookingPreparationTimes_ShouldThrowApplicationException_WithNotAvailableMessage()
        {
            // arrange
            var booking = _fixture.CreateBooking();
            var overlappedPreparationTimesBooking = new Booking
            {
                Nights = booking.Nights,
                RentalId = booking.RentalId,
                Start = booking.Start.AddDays(booking.Nights + booking.PreparationTimes.Count - 1)
            };

            // act
            var ex = Assert.Throws<ApplicationException>(() =>
            {
                _fixture.BookingBl.Create(booking);
            });

            // assert
            Assert.Equal(ResponseMessages.NotAvailable, ex.Message);
        }

        [Fact]
        public void Create_WithOneAvailableUnit_ShouldCreateBookingWithCorrectUnit()
        {
            // arrange
            var booking = _fixture.CreateBooking(2, 1);
            var newBooking = new Booking
            {
                Nights = booking.Nights,
                RentalId = booking.RentalId,
                Start = booking.Start
            };

            // act
            var id = _fixture.BookingBl.Create(newBooking);
            var bookedUnit = _fixture.ApplicationContext.Set<Booking>()[id].Unit;

            // assert
            Assert.Equal(booking.Unit + 1, bookedUnit);
        }

        [Fact]
        public void Create_WithRentalPreparationTime_ShouldCreateBookingWithCorrectPreparationTime()
        {
            // arrange
            int unitPreparationTimeInDays = 2;
            var booking = _fixture.CreateBooking(2, 1, unitPreparationTimeInDays);
            var newBooking = new Booking
            {
                Nights = booking.Nights,
                RentalId = booking.RentalId,
                Start = booking.Start
            };

            // act
            var id = _fixture.BookingBl.Create(newBooking);
            var createBooking = _fixture.ApplicationContext.Set<Booking>()[id];

            // assert
            Assert.Equal(unitPreparationTimeInDays, createBooking.PreparationTimes.Count);
        }
    }
}
