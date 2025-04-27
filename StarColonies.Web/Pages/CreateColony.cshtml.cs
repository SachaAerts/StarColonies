using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Repositories;
using StarColonies.Domains.Services.pictures;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Entities.Items;
using StarColonies.Web.wwwroot.models;

namespace StarColonies.Web.Pages;

public class CreateColony(IColonistRepository colonistRepository, IColonyRepository colonyRepository)
    : PageModel
{
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public NewColony NewColony { get; set; }

    public IList<ColonistModel> Colonists { get; set; } = new List<ColonistModel>();
    
    public ColonistModel TeamOwner { get; set; }

    public async Task<IActionResult> OnGet()
    {
        TeamOwner = await colonistRepository.GetColonistByIdAsync(Id.ToString());
        await GetAvailableColonists();
        NewColony ??= new NewColony
        {
            Colonists = new List<ColonistModel>()
        };
        return Page();
    }

    private async Task GetAvailableColonists()
    {
        var usersList = await colonistRepository.GetColonistsAsync();
        Colonists = usersList
            .Where(c => c.Id != Id.ToString())
            .ToList();
    }

    public async Task<IActionResult> OnPost()
    {
        TeamOwner = await colonistRepository.GetColonistByIdAsync(Id.ToString());
        Colonists = await colonistRepository.GetColonistsAsync();

        var colonistIds = NewColony.ColonistsId;
        colonistIds.Add(Id);
        
        NewColony.Colonists = GetSelectedColonists(colonistIds);

        ModelState.Remove("NewColony.Colonists");
        TryValidateModel(NewColony, nameof(NewColony));

        if (!ModelState.IsValid)
        {
            await GetAvailableColonists();
            NewColony.Colonists = new List<ColonistModel> { TeamOwner };
            return Page();
        }
        
        AnalyzeProfilePicture analyzeProfilePicture = new AnalyzeProfilePicture(NewColony.Name);

        ColonyModel colony = new ColonyModel()
        {
            Name = NewColony.Name,
            OwnerId = TeamOwner.Id.ToString(),
            LogoPath = analyzeProfilePicture.GetProfilePictureFileName(NewColony.PictureTeam),
            Colonists = NewColony.Colonists
        };
        
        await colonyRepository.AddColonyAsync(colony);
        
        return RedirectToPage("/Profile", new { id = Id });
    }

    private IList<ColonistModel> GetSelectedColonists(IList<Guid> colonistIds)
    {
        return Colonists
            .Where(c => colonistIds.Contains(Guid.Parse(c.Id)))
            .ToList();
    }
}