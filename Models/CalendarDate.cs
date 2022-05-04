using System;
using System.Collections.Generic;

namespace Models
{
    public class CalendarDate
    {
        public DateTime Date { get; set; }

        public List<CalendarBooking> Bookings { get; set; }
    }
}
