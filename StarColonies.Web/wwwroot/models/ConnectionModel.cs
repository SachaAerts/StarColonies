using System.ComponentModel.DataAnnotations;

namespace StarColonies.Web.wwwroot.models;

public class ConnectionModel
{
    [Required(ErrorMessage = "Email required")]
    public string EmailOrUsernameConnection { get; set; } = "";
    
    [Required(ErrorMessage = "Password required")]
    public string PasswordConnection { get; set; } = "";
}