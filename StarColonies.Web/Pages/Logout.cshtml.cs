using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Infrastructures.Data.dataclass;

namespace StarColonies.Web.Pages;

public class Logout(SignInManager<Colonist> signInManager) : PageModel
{
    public async Task<IActionResult> OnPost()
    {
        await signInManager.SignOutAsync();
        return RedirectToPage("/Index");
    }
}