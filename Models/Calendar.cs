using Models.Interfaces;
using System.Collections.Generic;

namespace Models
{
    public class Calendar : IEntity
    {
        public int Id { get; set; }

        public int RentalId { get; set; }

        public List<CalendarDate> Dates { get; set; }
    }
}
