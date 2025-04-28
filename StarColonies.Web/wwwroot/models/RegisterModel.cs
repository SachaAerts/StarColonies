using System.ComponentModel.DataAnnotations;
using StarColonies.Web.Validators;

namespace StarColonies.Web.wwwroot.models;

public class RegisterModel
{
    [Required(ErrorMessage = "Email required")]
    [EmailFormat(ErrorMessage = "Invalid email")]
    public required string EmailRegister { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password required")]
    public required string PasswordRegister { get; set; }
    
    [Required(ErrorMessage = "ConfirmPassword required")]
    [Compare("PasswordRegister", ErrorMessage = "Password and confirm password do not match")]
    public required string ConfirmPasswordRegister { get; set; }
}