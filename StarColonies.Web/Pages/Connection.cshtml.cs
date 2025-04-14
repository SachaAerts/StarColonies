using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models;
using StarColonies.Infrastructures.Data.dataclass;
using StarColonies.Web.wwwroot.models;

namespace StarColonies.Web.Pages;

public class Connection(SignInManager<Colonist> signInManager, UserManager<Colonist> userManager)
    : PageModel
{
    [BindProperty] 
    public ConnectionModel ConnectionUser { get; set; } = new();
    
    [BindProperty] 
    public RegisterModel RegisterUser { get; set; }
    
    public void OnGet()
    {
    }
    
    public async Task<IActionResult> OnPostLoginAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

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

    public async Task<IActionResult> OnPostRegister()
    {
        if (!ModelState.IsValid)
            return Page();

        var existingUser = await userManager.FindByEmailAsync(RegisterUser.EmailRegister);
        if (existingUser != null)
        {
            ModelState.AddModelError("RegisterUser.EmailRegister", "This mail is ever used");
            return Page();
        }

        var fakeUser = new Colonist
        {
            Email = RegisterUser.EmailRegister, 
            UserName = RegisterUser.EmailRegister,
            DateOfBirth = DateTime.Now, 
            JobModel = JobModel.Engineer,
            Level = 1,
            Strength = 1,
            Endurance = 1,
            Musty = 0
        };
        var pwdCheck = await userManager.PasswordValidators[0].ValidateAsync(userManager, fakeUser, RegisterUser.PasswordRegister);

        if (!pwdCheck.Succeeded)
        {
            foreach (var error in pwdCheck.Errors)
                ModelState.AddModelError("RegisterUser.PasswordRegister", error.Description);
            return Page();
        }

        TempData["Email"] = RegisterUser.EmailRegister;
        TempData["Password"] = RegisterUser.PasswordRegister;

        return RedirectToPage("CreateColon");
    }
}