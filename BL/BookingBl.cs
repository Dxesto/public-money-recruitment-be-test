using BL.Interfaces;
using Common;
using DAL.Repositories;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
                throw new ApplicationException(ResponseMessages.EntityNotFound(ResourceTypes.Booking));

            return _unitOfWork.BookingRepository.GetById(id);
        }

        public int Create(Booking booking)
        {
            if (!_unitOfWork.RentalRepository.Any(booking.RentalId))
                throw new ApplicationException(ResponseMessages.EntityNotFound(ResourceTypes.Rental));

            var rental = _unitOfWork.RentalRepository.GetById(booking.RentalId);

            DateTime startDate = booking.Start;
            DateTime endDateTime = booking.Start.AddDays(booking.Nights + rental.PreparationTimeInDays);

            Func<Booking, bool> filter = b =>
                        b.RentalId == booking.RentalId
                        && (b.Start.Date <= startDate.Date && b.Start.AddDays(b.Nights + b.PreparationTimes.Count).Date > startDate.Date)
                        || (b.Start.Date < endDateTime.Date && b.Start.AddDays(b.Nights + b.PreparationTimes.Count).Date >= endDateTime.Date)
                        || (b.Start.Date > startDate.Date && b.Start.AddDays(b.Nights + b.PreparationTimes.Count).Date < endDateTime.Date);

            List<int> overlappedBookingsUnit = _unitOfWork.BookingRepository.Get(filter).Select(x => x.Unit).ToList();

            booking.Unit = GetAvailableUnit(overlappedBookingsUnit, rental);
            booking.PreparationTimes = Enumerable.Range(0, rental.PreparationTimeInDays).Select(x => new PreparationTime { Unit = booking.Unit }).ToList();

            var id = _unitOfWork.BookingRepository.Add(booking);

            return id;
        }

        private int GetAvailableUnit(List<int> overlappedBookingsUnit, Rental rental)
        {
            if (!overlappedBookingsUnit.Any())
            {
                return 1;
            }

            for (int i = 1; i <= rental.Units; i++)
            {
                if (!overlappedBookingsUnit.Any(x => x == i))
                {
                    return i;
                }
            }

            throw new ApplicationException(ResponseMessages.NotAvailable);
        }
    }
}
