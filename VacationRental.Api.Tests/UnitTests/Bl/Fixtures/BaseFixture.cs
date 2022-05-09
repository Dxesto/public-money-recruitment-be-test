using DAL;
using DAL.Repositories;
using Models;
using System;
using System.Linq;

namespace VacationRental.Api.Tests.UnitTests.Bl.Fixtures
{
    public class BaseFixture
    {
        protected UnitOfWork UnitOfWork;

        public ApplicationContext ApplicationContext;

        public BaseFixture()
        {
            ApplicationContext = new ApplicationContext();
            UnitOfWork = new UnitOfWork(ApplicationContext);
        }

        public int GetRandomId()
        {
            Random rnd = new Random();
            return rnd.Next(int.MinValue, int.MaxValue);
        }

        public Booking CreateBooking(int unitInRental = 1, int unit = 1, int unitPreparationTimeInDays = 2)
        {
            int rentalId = GetRandomId();

            ApplicationContext.Set<Rental>().Add(rentalId, new Rental { Id = rentalId, PreparationTimeInDays = unitPreparationTimeInDays, Units = unitInRental });

            var bookingData = ApplicationContext.Set<Booking>();

            int bookingId = GetRandomId();
            Booking booking = new Booking
            {
                Id = bookingId,
                RentalId = rentalId,
                Nights = 3,
                Unit = unit,
                Start = DateTime.Now,
                PreparationTimes = Enumerable.Range(0, unitPreparationTimeInDays).Select(x => new PreparationTime { Unit = x }).ToList()
            };

            bookingData.Add(bookingId, booking);

            return booking;
        }
    }
}
