using System.Collections.Generic;

namespace Models
{
    public class Calendar
    {
        public int RentalId { get; set; }

        public List<CalendarDate> Dates { get; set; }
    }
}
