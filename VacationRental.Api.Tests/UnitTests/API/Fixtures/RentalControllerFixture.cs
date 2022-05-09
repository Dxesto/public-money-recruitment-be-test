using AutoMapper;
using BL.Interfaces;
using Models;
using Moq;
using VacationRental.Api.Controllers;

namespace VacationRental.Api.Tests.UnitTests.API.Fixtures
{
    public class RentalControllerFixture
    {
        public readonly RentalsController Controller;

        public readonly Mock<IRentalBl> RentalBlMock;

        public RentalControllerFixture()
        {
            RentalBlMock = new Mock<IRentalBl>();

            RentalBlMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Rental());
            RentalBlMock.Setup(x => x.Create(It.IsAny<Rental>())).Returns(() => default);

            Controller = new RentalsController(RentalBlMock.Object, new Mock<IMapper>().Object);
        }
    }
}
