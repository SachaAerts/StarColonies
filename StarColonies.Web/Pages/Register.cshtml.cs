using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Web.wwwroot.models;

namespace StarColonies.Web.Pages;

public class Register(UserManager<ColonistEntity> userManager)
    :PageModel
{
    [BindProperty] 
    public required RegisterModel RegisterUser { get; set; }
    
    public void OnGet()
    {
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

        var fakeUser = new ColonistEntity()
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