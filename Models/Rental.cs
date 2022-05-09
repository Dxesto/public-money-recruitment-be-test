﻿using Models.Interfaces;

namespace Models
{
    public class Rental : IEntity
    {
        public int Id { get; set; }

        public int Units { get; set; }

        public int PreparationTimeInDays { get; set; }
    }
}
