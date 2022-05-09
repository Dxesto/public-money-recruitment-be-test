using AutoMapper;
using BL.Interfaces;
using Models;
using Moq;
using VacationRental.Api.Controllers;

namespace VacationRental.Api.Tests.UnitTests.API.Fixtures
{
    public class BookingControllerFixture
    {
        public readonly BookingsController Controller;

        public readonly Mock<IBookingBl> BookingBlMock;

        public BookingControllerFixture()
        {
            BookingBlMock = new Mock<IBookingBl>();

            BookingBlMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Booking());
            BookingBlMock.Setup(x => x.Create(It.IsAny<Booking>())).Returns(() => default);

            Controller = new BookingsController(BookingBlMock.Object, new Mock<IMapper>().Object);
        }

        public void Clear()
        {
            BookingBlMock.Invocations.Clear();
        }
    }
}
