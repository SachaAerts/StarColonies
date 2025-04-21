using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Repositories;
using StarColonies.Domains.Services.pictures;
using StarColonies.Web.wwwroot.models;

namespace StarColonies.Web.Pages;

public class ModifyColon(IColonistRepository colonistRepository)
    : PageModel
{
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    
    public required ColonistModel Colonist { get; set; }
    
    [BindProperty]
    public NewUser NewUser { get; set; }
    
    public async Task<IActionResult> OnGet()
    {
        if (!User.Identity?.IsAuthenticated ?? true)
            return Forbid();
        
        Console.WriteLine("id: " + Id.ToString());
        Colonist = await colonistRepository.GetColonistByIdAsync(Id.ToString());
        
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        AnalyzeProfilePicture analyzeProfilePicture = new AnalyzeProfilePicture(NewUser.SettlerName);
        
        var colonist = new ColonistModel()
        {
            Id = Colonist.Id,
            Name = NewUser.SettlerName,
            Email = NewUser.Email,
            DateOfBirth = DateTime.ParseExact(NewUser.BirthdayEntry, "dd/MM/yyyy", CultureInfo.InvariantCulture),
            Job = Enum.Parse<JobModel>(NewUser.Profession),
            Level = Colonist.Level,
            Strength = GetStrength(NewUser.Statistics),
            Stamina = GetStamina(NewUser.Statistics),
            Musty = Colonist.Musty,
            ProfilPicture = analyzeProfilePicture.GetProfilePictureFileNameForUpdate(NewUser.ProfilePicture, Colonist.ProfilPicture)
        };
        
        Console.WriteLine("user: " + colonist);
        
        //await colonistRepository.UpdateColonistAsync(colonist);
        
        return RedirectToPage("/Profile/Index", new { id = colonist.Id });
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