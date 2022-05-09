using BL.Interfaces;
using Common;
using DAL.Repositories;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public class CalendarBl : ICalendarBl
    {
        private readonly UnitOfWork _unitOfWork;

        public CalendarBl(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Calendar Get(int rentalId, DateTime start, int nights)
        {
            if (!_unitOfWork.RentalRepository.Any(rentalId))
                throw new ApplicationException(ResponseMessages.EntityNotFound(ResourceTypes.Rental));

            var rental = _unitOfWork.RentalRepository.GetById(rentalId);

            var result = new Calendar
            {
                RentalId = rentalId,
                Dates = new List<CalendarDate>()
            };

            DateTime startDate = start;
            DateTime endDateTime = start.AddDays(nights + rental.PreparationTimeInDays);

            Func<Booking, bool> filter = b =>
                        b.RentalId == rentalId
                        && (b.Start.Date <= startDate.Date && b.Start.AddDays(b.Nights + b.PreparationTimes.Count).Date > startDate.Date)
                        || (b.Start.Date < endDateTime.Date && b.Start.AddDays(b.Nights + b.PreparationTimes.Count).Date >= endDateTime.Date)
                        || (b.Start.Date > startDate.Date && b.Start.AddDays(b.Nights + b.PreparationTimes.Count).Date < endDateTime.Date);

            List<Booking> overlappedBookings = _unitOfWork.BookingRepository.Get(filter).ToList();

            for (int i = 0; i < nights + rental.PreparationTimeInDays; i++)
            {
                var date = start.Date.AddDays(i);

                var dayOverlappedBookings = GetOverlappedBookings(date, rentalId, overlappedBookings);

                var dayOverlappedPreparationTimeBookings = GetOverlappedPreparationTimes(date, rentalId, overlappedBookings);

                var calendarDate = new CalendarDate
                {
                    Date = start.Date.AddDays(i)
                };

                calendarDate.Bookings.AddRange(dayOverlappedBookings.Select(x => new CalendarBooking { Id = x.Id, Unit = x.Unit }));
                calendarDate.PreparationTimes.AddRange(dayOverlappedPreparationTimeBookings.Select(x => new PreparationTime { Unit = x.Unit }));

                result.Dates.Add(calendarDate);
            }

            return result;
        }

        private IList<Booking> GetOverlappedBookings(DateTime date, int rentalId , IList<Booking> overlappedBookings)
        {
            Func<Booking, bool> dayOverlappedBookingsFilter = booking => booking.RentalId == rentalId
                && booking.Start.Date <= date.Date && booking.Start.AddDays(booking.Nights).Date > date.Date;

            IEnumerable<Booking> dayOverlappedBookings = overlappedBookings.Where(dayOverlappedBookingsFilter).ToList();

            return dayOverlappedBookings.ToList();
        }

        private IList<Booking> GetOverlappedPreparationTimes(DateTime date, int rentalId, IList<Booking> overlappedBookings)
        {
            Func<Booking, bool> preparationTimeBookingsFilter = booking => booking.RentalId == rentalId
                && booking.Start.AddDays(booking.Nights).Date <= date.Date && booking.Start.AddDays(booking.Nights + booking.PreparationTimes.Count).Date > date.Date;

            IEnumerable<Booking> preparationTimeBookings = overlappedBookings.Where(preparationTimeBookingsFilter);

            return preparationTimeBookings.ToList();
        }
    }
}
