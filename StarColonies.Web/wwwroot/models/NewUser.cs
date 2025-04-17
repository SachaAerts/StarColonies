using System.ComponentModel.DataAnnotations;
using StarColonies.Web.validators;
using StarColonies.Web.Validators;

namespace StarColonies.Web.wwwroot.models;

public class NewUser
{
    public string Email { get; set; } = "";
    
    public string Password { get; set; } = "";
    
    [Required(ErrorMessage = "Settler's name is required")]
    public string SettlerName { get; set; } = "";

    [Required(ErrorMessage = "Birthdate is required")]
    [DateFormat("dd/MM/yyyy", ErrorMessage = "Birthdate format is dd/MM/yyyy")]
    [DateNotIntFuture(ErrorMessage = "Birthdate cannot be in the future")]
    public string BirthdayEntry { get; set; } = "";
    
    [Required(ErrorMessage = "Profession is required")]
    public string Profession { get; set; } = "";
    
    [Required(ErrorMessage = "Profile picture is required")]
    public string ProfilePicture { get; set; } = "";
    
    [StatsRegister(ErrorMessage = "Assign all available levels")]
    public string Statistics { get; set; } = "";
}