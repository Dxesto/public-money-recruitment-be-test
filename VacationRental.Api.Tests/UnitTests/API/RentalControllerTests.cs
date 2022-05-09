using Models;
using Moq;
using VacationRental.Api.Tests.UnitTests.API.Fixtures;
using Xunit;

namespace VacationRental.Api.Tests.UnitTests.API
{
    public class RentalControllerTests : IClassFixture<RentalControllerFixture>
    {
        private readonly RentalControllerFixture _rentalFixture;

        public RentalControllerTests(RentalControllerFixture rentalFixture)
        {
            _rentalFixture = rentalFixture;
        }

        [Fact]
        public void Get_WithValidData_ShouldReturnEntity()
        {
            // arrange and act
            var result = _rentalFixture.Controller.Get(default);

            // assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Post_WithValidData_ShouldReturnResourceIdDto()
        {
            // arrange and act
            var result = _rentalFixture.Controller.Post(default);

            // assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Get_WithValidData_ShouldCallBl_Once()
        {
            // arrange and act
            var result = _rentalFixture.Controller.Get(default);

            // assert
            _rentalFixture.RentalBlMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void Post_WithValidData_ShouldCallBl_Once()
        {
            // arrange and act
            var result = _rentalFixture.Controller.Post(default);

            // assert
            _rentalFixture.RentalBlMock.Verify(x => x.Create(It.IsAny<Rental>()), Times.Once);
        }
    }
}
