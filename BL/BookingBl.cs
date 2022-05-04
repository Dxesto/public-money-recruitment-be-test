using BL.Interfaces;
using DAL.Repositories;
using Models;
using System;

namespace BL
{
    public class BookingBl : IBookingBl
    {
        private readonly UnitOfWork _unitOfWork;

        public BookingBl(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Booking GetById(int id)
        {
            if (!_unitOfWork.BookingRepository.Any(id))
                throw new ApplicationException("Booking not found");

            return _unitOfWork.BookingRepository.GetById(id);
        }

        public int Create(Booking booking)
        {
            if (!_unitOfWork.RentalRepository.Any(booking.RentalId))
                throw new ApplicationException("Rental not found");

            for (var i = 0; i < booking.Nights; i++)
            {
                Func<Booking, bool> filter = b => b.RentalId == booking.RentalId
                        && (b.Start <= booking.Start.Date && b.Start.AddDays(b.Nights) > booking.Start.Date)
                        || (b.Start < booking.Start.AddDays(booking.Nights) && b.Start.AddDays(b.Nights) >= booking.Start.AddDays(booking.Nights))
                        || (b.Start > booking.Start && b.Start.AddDays(b.Nights) < booking.Start.AddDays(booking.Nights));

                int count = _unitOfWork.BookingRepository.Count(filter);

                if (count >= _unitOfWork.RentalRepository.GetById(booking.RentalId).Units)
                    throw new ApplicationException("Not available");
            }

            var id = _unitOfWork.BookingRepository.Add(booking);

            return id;
        }
    }
}
