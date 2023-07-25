using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models;
public class LoginViewModel
{
    [Required(ErrorMessage = "Please enter your email address")]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "Please enter your password")]
    [UIHint("Password")]
    [PasswordPropertyText]
    public string Password { get; set; } = null!;
    public string? ReturnUrl { get; set; }
}
