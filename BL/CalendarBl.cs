using BL.Interfaces;
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
                throw new ApplicationException("Rental not found");

            var result = new Calendar
            {
                RentalId = rentalId,
                Dates = new List<CalendarDate>()
            };

            for (var i = 0; i < nights; i++)
            {
                var date = new CalendarDate
                {
                    Date = start.Date.AddDays(i),
                    Bookings = new List<CalendarBooking>()
                };

                Func<Booking, bool> filter = booking => booking.RentalId == rentalId
                        && booking.Start <= date.Date && booking.Start.AddDays(booking.Nights) > date.Date;

                IList<Booking> bookings = _unitOfWork.BookingRepository.Get(filter);

                var calendarBookings = bookings.Select(x => new CalendarBooking { Id = x.Id });

                date.Bookings.AddRange(calendarBookings);

                result.Dates.Add(date);
            }

            return result;
        }
    }
}
