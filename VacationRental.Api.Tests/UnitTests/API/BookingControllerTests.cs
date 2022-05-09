using Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using VacationRental.Api.Tests.UnitTests.API.Fixtures;
using Xunit;

namespace VacationRental.Api.Tests.UnitTests.API
{
    public class BookingControllerTests : IClassFixture<BookingControllerFixture>, IDisposable
    {
        private readonly BookingControllerFixture _bookingControllerFixture;

        public BookingControllerTests(BookingControllerFixture bookingControllerFixture)
        {
            _bookingControllerFixture = bookingControllerFixture;
        }

        [Fact]
        public void Get_WithValidData_ShouldReturnEntity()
        {
            // arrange and act
            var result = _bookingControllerFixture.Controller.Get(default);

            // assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Post_WithValidData_ShouldReturnResourceIdDto()
        {
            // arrange and act
            var result = _bookingControllerFixture.Controller.Post(default);

            // assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Get_WithValidData_ShouldCallBl_Once()
        {
            // arrange and act
            var result = _bookingControllerFixture.Controller.Get(default);

            // assert
            _bookingControllerFixture.BookingBlMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void Post_WithValidDataa_ShouldCallBl_Once()
        {
            // arrange and act
            var result = _bookingControllerFixture.Controller.Post(default);

            // assert
            _bookingControllerFixture.BookingBlMock.Verify(x => x.Create(It.IsAny<Booking>()), Times.Once);
        }

        public void Dispose()
        {
            _bookingControllerFixture.Clear();
        }
    }
}
