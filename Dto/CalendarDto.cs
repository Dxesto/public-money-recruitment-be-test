using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class CalendarDto : IValidatableObject
    {
        public int RentalId { get; set; }

        public DateTime Start { get; set; }

        public int Nights { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();

            if (Nights <= 0)
                validationResults.Add(new ValidationResult("Nigts must be positive"));

            return validationResults;
        }
    }
}
