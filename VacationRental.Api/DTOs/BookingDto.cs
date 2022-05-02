﻿using System;

namespace VacationRental.Api.DTOs
{
    public class BookingDto
    {
        public int RentalId { get; set; }

        public DateTime Start { get; set; }

        public int Nights { get; set; }
    }
}
