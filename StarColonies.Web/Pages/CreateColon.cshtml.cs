using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models;
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
            ProfilPicture = GetProfilePictureFileName(NewUser.ProfilePicture)
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

    private string GetProfilePictureFileName(string picture)
    {
        if (string.IsNullOrWhiteSpace(picture))
            return "1.png";

        // Cas 1 : URL ou chemin déjà défini
        if (picture.StartsWith("/") || picture.StartsWith("http"))
        {
            var fileName = Path.GetFileName(picture);
            return string.IsNullOrWhiteSpace(fileName) ? "1.png" : fileName;
        }

        // Cas 2 : base64
        if (picture.StartsWith("data:image"))
        {
            var base64Data = picture.Substring(picture.IndexOf(',') + 1);
            var bytes = Convert.FromBase64String(base64Data);

            var extension = GetImageExtension(picture);
            var fileName = GenerateUniqueFileName(NewUser.SettlerName, extension);
            var uploadDir = Path.Combine("wwwroot", "img", "upload");

            if (!Directory.Exists(uploadDir))
                Directory.CreateDirectory(uploadDir);

            var fullPath = Path.Combine(uploadDir, fileName);

            // Gestion des doublons
            while (System.IO.File.Exists(fullPath))
            {
                fileName = GenerateUniqueFileName(NewUser.SettlerName, extension, forceGuid: true);
                fullPath = Path.Combine(uploadDir, fileName);
            }

            System.IO.File.WriteAllBytes(fullPath, bytes);
            return fileName;
        }

        return "1.png";
    }

    private string GenerateUniqueFileName(string baseName, string extension, bool forceGuid = false)
    {
        var sanitized = SanitizeFileName(baseName);
        if (forceGuid)
        {
            return $"{sanitized}_{Guid.NewGuid()}{extension}";
        }

        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return $"{sanitized}_{timestamp}{extension}";
    }

    private string GetImageExtension(string picture)
    {
        if (picture.StartsWith("data:image/jpeg")) return ".jpg";
        if (picture.StartsWith("data:image/png")) return ".png";
        if (picture.StartsWith("data:image/gif")) return ".gif";
        return ".png";
    }

    private string SanitizeFileName(string input)
    {
        foreach (var c in Path.GetInvalidFileNameChars())
        {
            input = input.Replace(c, '_');
        }
        return input.Replace(" ", "_").ToLowerInvariant();
    }
}
