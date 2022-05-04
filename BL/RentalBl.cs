using BL.Interfaces;
using DAL.Repositories;
using Models;
using System;

namespace BL
{
    public class RentalBl : IRentalBl
    {
        private readonly UnitOfWork _unitOfWork;

        public RentalBl(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Rental GetById(int id)
        {
            if (!_unitOfWork.RentalRepository.Any(id))
                throw new ApplicationException("Rental not found");

            return _unitOfWork.RentalRepository.GetById(id);
        }

        public int Create(Rental rental)
        {
            var id = _unitOfWork.RentalRepository.Add(rental);

            return id;
        }
    }
}
