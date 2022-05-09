using Models.Interfaces;
using System;
using System.Collections.Generic;

namespace Models
{
    public class Booking : IEntity
    {
        public int Id { get; set; }

        public int RentalId { get; set; }

        public DateTime Start { get; set; }

        public int Nights { get; set; }

        public int Unit { get; set; }

        public IList<PreparationTime> PreparationTimes { get; set; } = new List<PreparationTime>();
    }
}
