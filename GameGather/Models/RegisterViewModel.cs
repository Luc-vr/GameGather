using Domain.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Validation;

namespace Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter your first name")]
        [MaxLength(255)]
        [Display(Name = "First name")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        [MaxLength(255)]
        [Display(Name = "Last name")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [MaxLength(255)]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one digit.")]
        [UIHint("Password")]
        [PasswordPropertyText]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Please confirm your password")]
        [UIHint("Password")]
        [PasswordPropertyText]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; } = null!;

        [Required(ErrorMessage = "Please pick your gender")]
        [EnumDataType(typeof(Gender), ErrorMessage = "Please pick your gender")]
        [Display(Name = "Gender")]
        public Gender? Gender { get; set; }

        [Required(ErrorMessage = "Please enter your birth date")]
        [DataType(DataType.Date)]
        [Display(Name = "Birth date")]
        [NotInFuture(ErrorMessage = "Birth date cannot be in the future.")]
        [MinimumAge(16, ErrorMessage = "You must be at least 16 years old.")]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "Please enter the name of your city")]
        [Display(Name = "City")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Please enter the address")]
        [RegularExpression(@"^[A-Za-z\s]+ \d+[A-Za-z]{0,2}$", ErrorMessage = "Invalid address format. Example: Street 123 or Street 1A")]
        [Display(Name = "Address")]
        public string? Address { get; set; }


    }
}
