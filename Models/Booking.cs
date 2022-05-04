using Models.Interfaces;
using System;

namespace Models
{
    public class Booking : IEntity
    {
        public int Id { get; set; }

        public int RentalId { get; set; }

        public DateTime Start { get; set; }

        public int Nights { get; set; }
    }
}
