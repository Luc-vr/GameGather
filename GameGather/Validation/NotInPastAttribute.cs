using System.ComponentModel.DataAnnotations;

namespace Web.Validation
{
    public class NotInPastAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dateTime && dateTime < DateTime.Now)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success!;
        }
    }
}