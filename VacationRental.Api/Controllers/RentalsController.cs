﻿using AutoMapper;
using BL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Models;
using VacationRental.Api.Models;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalBl _rentalBl;
        private readonly IMapper _mapper;

        public RentalsController( IRentalBl rentalBl, IMapper mapper)
        {
            _rentalBl = rentalBl;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{rentalId:int}")]
        public Rental Get([FromRoute] int rentalId)
        {
            return _rentalBl.GetById(rentalId);
        }

        [HttpPost]
        public ResourceIdViewModel Post([FromBody] RentalDto rentalDto)
        {
            var rental = _mapper.Map<Rental>(rentalDto);

            var id = _rentalBl.Create(rental);

            return new ResourceIdViewModel { Id = id };
        }
    }
}
