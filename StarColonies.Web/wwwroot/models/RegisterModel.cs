using System.ComponentModel.DataAnnotations;

namespace StarColonies.Web.wwwroot.models;

public class RegisterModel
{
    [Required(ErrorMessage = "Email required")]
    [EmailAddress(ErrorMessage = "Invalid email")]
    public required string EmailRegister { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password required")]
    public required string PasswordRegister { get; set; }
    
    [Required(ErrorMessage = "ConfirmPassword required")]
    [Compare("PasswordRegister", ErrorMessage = "Password and confirm password do not match")]
    public required string ConfirmPasswordRegister { get; set; }
}