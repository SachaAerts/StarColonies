using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Web.wwwroot.models;

namespace StarColonies.Web.Pages.connection;

public class Connection : PageModel
{
    [BindProperty] public ConnectionModel ConnectionUser { get; set; } = new();
    
    [BindProperty] public RegisterModel RegisterUser { get; set; } = new();
    
    public void OnGet()
    {
    }
    
    public IActionResult OnPostLogin()
    {
        Console.WriteLine($"[DEBUG] Username: {ConnectionUser.EmailOrUsernameConnection}, Password: {ConnectionUser.PasswordConnection}");
        
        return Page();
    }

    public IActionResult OnPostRegister()
    {
        TempData["Email"] = RegisterUser.EmailRegister;
        TempData["Password"] = RegisterUser.PasswordRegister;

        return RedirectToPage("/connection/CreateColon");
    }
}