using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Web.ViewComponents;

public class ColonistProfileViewComponent(UserManager<ColonistEntity> userManager) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        if (!User.Identity?.IsAuthenticated ?? true)
            return View(null); // utilisateur non connecté

        var colonist = await userManager.GetUserAsync((System.Security.Claims.ClaimsPrincipal)User);
        return View(colonist);
    }
}