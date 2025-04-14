using System.ComponentModel.DataAnnotations;

namespace StarColonies.Web.wwwroot.models;

public class RegisterModel
{
    [Required]
    public string EmailRegister { get; set; } = "";
    
    [Required]
    public string PasswordRegister { get; set; } = "";
    
    public string ConfirmPasswordRegister { get; set; } = "";
}