using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Web.wwwroot.models;

namespace StarColonies.Web.Pages.connection;

public class Connection : PageModel
{
    [BindProperty] public ConnectionRegisterModel ConnectionRegister { get; set; } = new();
    
    public void OnGet()
    {
    }
    
    public IActionResult OnPostLogin()
    {
        Console.WriteLine($"[DEBUG] Username: {ConnectionRegister.EmailOrUsernameConnection}, Password: {ConnectionRegister.PasswordConnection}");
        
        return Page();
    }

    public IActionResult OnPostRegister()
    {
        Console.WriteLine($"[DEBUG] Email: {ConnectionRegister.EmailRegister}, Password: {ConnectionRegister.PasswordRegister}, PasswordConfirmation: {ConnectionRegister.ConfirmPasswordRegister}");
        
        return Page();
    }
}