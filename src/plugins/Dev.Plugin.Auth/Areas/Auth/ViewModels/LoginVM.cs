using System.ComponentModel.DataAnnotations;
using Dev.Plugin.Auth.Helpers.Validators;
using Microsoft.AspNetCore.Authentication;

namespace Dev.Plugin.Auth.Areas.Auth.ViewModels;

public class LoginVM
{
    [Required(ErrorMessage = "Email address is required")]
    //[CustomEmailValidator(ErrorMessage = "Email address is not valid (custom)")]
    public string EmailAddress { get; set; }


    [Required(ErrorMessage = "Password is required")]
    [MinLength(5, ErrorMessage = "Password must be at least 5 characters")]
    public string Password { get; set; }

    //Third-party login providers
    public IEnumerable<AuthenticationScheme> Schemes { get; set; }
}
