using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Infrastructures.Data.dataclass;
using StarColonies.Web.wwwroot.models;

namespace StarColonies.Web.Pages;

public class Connection(SignInManager<Colonist> signInManager, UserManager<Colonist> userManager)
    : PageModel
{
    [BindProperty] 
    public ConnectionModel ConnectionUser { get; set; } = new();
    
    [BindProperty] 
    public RegisterModel RegisterUser { get; set; } = new();
    
    public void OnGet()
    {
    }
    
    public async Task<IActionResult> OnPostLoginAsync()
    {
        if (!ModelState.IsValid)
            return Page();
        
        var user = await userManager.FindByEmailAsync(ConnectionUser.EmailOrUsernameConnection)
                   ?? await userManager.FindByNameAsync(ConnectionUser.EmailOrUsernameConnection);

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "User not found.");
            return Page();
        }

        var result = await signInManager.PasswordSignInAsync(user, ConnectionUser.PasswordConnection, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return RedirectToPage("/Index");
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return Page();
    }

    public IActionResult OnPostRegister()
    {
        TempData["Email"] = RegisterUser.EmailRegister;
        TempData["Password"] = RegisterUser.PasswordRegister;

        return RedirectToPage("CreateColon");
    }
}