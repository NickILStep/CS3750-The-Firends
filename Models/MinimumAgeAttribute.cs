using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment1v3.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime birthDate)
            {
                int age = DateTime.Now.Year - birthDate.Year;

                // Adjust age if birth date hasn't occurred yet this year
                if (birthDate.Date > DateTime.Now.Date.AddYears(-age))
                {
                    age--;
                }

                if (age < _minimumAge)
                {
                    return new ValidationResult($"You must be at least {_minimumAge} years old.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
