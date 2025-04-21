using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models;
using StarColonies.Domains.Services.pictures;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Web.wwwroot.models;

namespace StarColonies.Web.Pages;

public class CreateColon(UserManager<ColonistEntity> userManager, SignInManager<ColonistEntity> signInManager)
    : PageModel
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

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
            return Page();
        
        AnalyzeProfilePicture analyzeProfilePicture = new AnalyzeProfilePicture(NewUser.SettlerName);

        var colonist = new ColonistEntity()
        {
            UserName = NewUser.SettlerName,
            Email = NewUser.Email,
            DateOfBirth = DateTime.ParseExact(NewUser.BirthdayEntry, "dd/MM/yyyy", CultureInfo.InvariantCulture),
            JobModel = Enum.Parse<JobModel>(NewUser.Profession),
            Level = 1,
            Strength = GetStrength(NewUser.Statistics),
            Stamina = GetStamina(NewUser.Statistics),
            Musty = 0,
            ProfilPicture = analyzeProfilePicture.GetProfilePictureFileName(NewUser.ProfilePicture)
        };

        var result = await userManager.CreateAsync(colonist, NewUser.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }

        await userManager.AddToRoleAsync(colonist, "Player");
        await signInManager.SignInAsync(colonist, isPersistent: false);

        return RedirectToPage("/Index");
    }

    private int GetStrength(string stats)
    {
        if (string.IsNullOrWhiteSpace(stats)) return -1;

        var parts = stats.Split('-');
        return (parts.Length == 2 && int.TryParse(parts[0], out int left)) ? left : -1;
    }

    private int GetStamina(string stats)
    {
        if (string.IsNullOrWhiteSpace(stats)) return -1;

        var parts = stats.Split('-');
        return (parts.Length == 2 && int.TryParse(parts[1], out int right)) ? right : -1;
    }
}
