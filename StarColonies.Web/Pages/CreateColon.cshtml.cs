using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Web.wwwroot.models;

namespace StarColonies.Web.Pages;

public class CreateColon : PageModel
{
    [TempData]
    public required string Email { get; set; } = "";
    
    [TempData]
    public required string Password { get; set; } = "";
    
    [BindProperty]
    public NewUser NewUser { get; set; } = new();
    
    public void OnGet()
    {
        NewUser.Email = Email;
        NewUser.Password = Password;
    }
}