using BL.Interfaces;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/calendar")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarBl _calendarBl;

        public CalendarController(ICalendarBl calendarBl)
        {
            _calendarBl = calendarBl;
        }

        [HttpGet]
        public Calendar Get([FromQuery] CalendarDto calendarDto)
        {
            var result = _calendarBl.Get(calendarDto.RentalId, calendarDto.Start, calendarDto.Nights);

            return result;
        }
    }
}
