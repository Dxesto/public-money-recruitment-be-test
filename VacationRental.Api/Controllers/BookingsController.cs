using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VacationRental.Api.DTOs;
using VacationRental.Api.Models;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IDictionary<int, Rental> _rentals;
        private readonly IDictionary<int, Booking> _bookings;

        public BookingsController(
            IDictionary<int, Rental> rentals,
            IDictionary<int, Booking> bookings)
        {
            _rentals = rentals;
            _bookings = bookings;
        }

        [HttpGet]
        [Route("{bookingId:int}")]
        public Booking Get(int bookingId)
        {
            if (!_bookings.ContainsKey(bookingId))
                throw new ApplicationException("Booking not found");

            return _bookings[bookingId];
        }

        [HttpPost]
        public ResourceIdViewModel Post(BookingDto bookingDto)
        {
            if (bookingDto.Nights <= 0)
                throw new ApplicationException("Nigts must be positive");
            if (!_rentals.ContainsKey(bookingDto.RentalId))
                throw new ApplicationException("Rental not found");

            for (var i = 0; i < bookingDto.Nights; i++)
            {
                var count = 0;
                foreach (var booking in _bookings.Values)
                {
                    if (booking.RentalId == bookingDto.RentalId
                        && (booking.Start <= bookingDto.Start.Date && booking.Start.AddDays(booking.Nights) > bookingDto.Start.Date)
                        || (booking.Start < bookingDto.Start.AddDays(bookingDto.Nights) && booking.Start.AddDays(booking.Nights) >= bookingDto.Start.AddDays(bookingDto.Nights))
                        || (booking.Start > bookingDto.Start && booking.Start.AddDays(booking.Nights) < bookingDto.Start.AddDays(bookingDto.Nights)))
                    {
                        count++;
                    }
                }
                if (count >= _rentals[bookingDto.RentalId].Units)
                    throw new ApplicationException("Not available");
            }


            var key = new ResourceIdViewModel { Id = _bookings.Keys.Count + 1 };

            _bookings.Add(key.Id, new Booking
            {
                Id = key.Id,
                Nights = bookingDto.Nights,
                RentalId = bookingDto.RentalId,
                Start = bookingDto.Start.Date
            });

            return key;
        }
    }
}
