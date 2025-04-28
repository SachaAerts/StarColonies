using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Repositories;
using StarColonies.Web.wwwroot.models;

namespace StarColonies.Web.Pages;

public class Connection(IAuthenticationRepository authenticationRepository) : PageModel
{
    [BindProperty] 
    public ConnectionModel ConnectionUser { get; set; } = new();
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var (success, errorMessage) = await authenticationRepository.SignInAsync(
            ConnectionUser.EmailOrUsernameConnection,
            ConnectionUser.PasswordConnection
        );
        
        if (success) return RedirectToPage("/Index");
        
        ModelState.AddModelError(string.Empty, errorMessage ?? "Authentication failed");
        return Page();
    }
}