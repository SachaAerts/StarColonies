using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Web.wwwroot.models;

namespace StarColonies.Web.Pages.connection;

public class CreateColon : PageModel
{
    [TempData]
    public required string Email { get; set; } = "";
    
    [TempData]
    public required string Password { get; set; } = "";
    
    [BindProperty]
    public RegisterModel RegisterUser { get; set; } = new();
    
    public void OnGet()
    {
        RegisterUser.EmailRegister = Email;
        RegisterUser.PasswordRegister = Password;
        RegisterUser.ConfirmPasswordRegister = Password;
    }
}