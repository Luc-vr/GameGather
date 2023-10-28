using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using Web.Validation;

namespace Web.Models
{
    public class ProfileViewModel
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

        [Required(ErrorMessage = "Please pick your gender")]
        [EnumDataType(typeof(Gender), ErrorMessage = "Please pick your gender")]
        [Display(Name = "Gender")]
        public Gender? Gender { get; set; }

        [Required(ErrorMessage = "Please enter your birth date")]
        [DataType(DataType.Date)]
        [Display(Name = "Birth date")]
        [NotInFuture(ErrorMessage = "Birth date cannot be in the future.")]
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
