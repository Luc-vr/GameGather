using System.ComponentModel.DataAnnotations;

namespace Web.Validation
{
    public class NotInFutureAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime birthDate)
            {
                if (birthDate > DateTime.Now)
                {
                    return new ValidationResult("Birth date cannot be in the future.");
                }
            }

            return ValidationResult.Success!;
        }

    }
}