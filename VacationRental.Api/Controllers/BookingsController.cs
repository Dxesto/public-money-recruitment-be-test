using AutoMapper;
using BL.Interfaces;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingBl _bookingBl;
        private readonly IMapper _mapper;

        public BookingsController(IBookingBl bookingBl, IMapper mapper)
        {
            _bookingBl = bookingBl;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{bookingId:int}")]
        public Booking Get([FromRoute] int bookingId)
        {
            return _bookingBl.GetById(bookingId);
        }

        [HttpPost]
        public ResourceIdDto Post([FromBody] BookingDto bookingDto)
        {
            var booking = _mapper.Map<Booking>(bookingDto);

            var id = _bookingBl.Create(booking);

            return new ResourceIdDto { Id = id };
        }
    }
}
