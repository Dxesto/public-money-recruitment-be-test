using Common;
using Models;
using System;
using VacationRental.Api.Tests.UnitTests.Bl.Fixtures;
using Xunit;

namespace VacationRental.Api.Tests.UnitTests.Bl
{
    public class RentalBlTests
    {
        private readonly RentalFixture _fixture;

        public RentalBlTests()
        {
            _fixture = new RentalFixture();
        }

        [Fact]
        public void GetById_WithInvalidId_ShouldThrowApplicationException()
        {
            // arrange
            int id = _fixture.GetRandomId();

            // act and assert
            Assert.Throws<ApplicationException>(() =>
            {
                _fixture.RentalBl.GetById(id);
            });
        }

        [Fact]
        public void GetById_WithInvalidId_ShouldReturnCorrectNotFoundMessage()
        {
            // arrange
            int id = _fixture.GetRandomId();

            // act
            var ex = Assert.Throws<ApplicationException>(() =>
            {
                _fixture.RentalBl.GetById(id);
            });

            // assert
            Assert.Equal(ResponseMessages.EntityNotFound(ResourceTypes.Rental), ex.Message);
        }

        [Fact]
        public void GetById_WithValidId_ShouldReturnRental()
        {
            // arrange
            int id = _fixture.GetRandomId();
            _fixture.ApplicationContext.Set<Rental>().Add(id, new Rental());

            // act
            Rental rental = _fixture.RentalBl.GetById(id);

            // assert
            Assert.NotNull(rental);
        }

        [Fact]
        public void Create_WithValidData_ShouldReturnRentalId()
        {
            // arrange
            Rental rental = new Rental();

            // act
            int id = _fixture.RentalBl.Create(rental);

            // assert
            Assert.NotEqual(default, id);
        }
    }
}
