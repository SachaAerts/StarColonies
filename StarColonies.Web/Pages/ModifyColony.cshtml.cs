using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Repositories;
using StarColonies.Domains.Services.pictures;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Services.picture;
using StarColonies.Web.wwwroot.models;

namespace StarColonies.Web.Pages;

[Authorize]
public class ModifyColony(UserManager<ColonistEntity> userManager, IColonistRepository colonistRepository, IColonyRepository colonyRepository)
    : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int TeamId { get; set; }
    
    [BindProperty]
    public NewColony ModifColony { get; set; }
    
    public IList<ColonistModel> Colonists { get; set; }
    
    public ColonyModel? Colony { get; set; }
    
    public ColonistModel TeamOwner { get; set; }
    
    public async Task<IActionResult> OnGetAsync()
    {
        var user = await userManager.GetUserAsync(HttpContext.User);
        Colony = await colonyRepository.GetColonyByIdAsync(TeamId);
        TeamOwner = await colonistRepository.GetColonistByIdAsync(Colony!.OwnerId);
        if (user!.Id != TeamOwner.Id)
        {
            return RedirectToPage("Index");
        }
        
        Colonists = await colonistRepository.GetColonistsAsync();
        
        Colonists = GetAvailableColonists(Colonists, Colony.Colonists);
        return Page();
    }

    private static IList<ColonistModel> GetAvailableColonists(IList<ColonistModel> allColonists, IList<ColonistModel> teamColonists)
    {
        var teamIds = new HashSet<string>(teamColonists.Select(c => c.Id));
        var availableColonists = allColonists
            .Where(c => !teamIds.Contains(c.Id))
            .ToList();

        return availableColonists;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Colony = await colonyRepository.GetColonyByIdAsync(TeamId);
        TeamOwner = await colonistRepository.GetColonistByIdAsync(Colony!.OwnerId);

        Colonists = await colonistRepository.GetColonistsAsync();

        var selectedIds = ModifColony.ColonistsId;

        if (!selectedIds.Contains(Guid.Parse(TeamOwner.Id)))
        {
            selectedIds.Add(Guid.Parse(TeamOwner.Id));
        }

        ModifColony.Colonists = Colonists
            .Where(c => selectedIds.Contains(Guid.Parse(c.Id)))
            .ToList();

        ModelState.Remove("ModifColony.Colonists");
        TryValidateModel(ModifColony, nameof(ModifColony));

        if (!ModelState.IsValid)
        {
            Colonists = GetAvailableColonists(Colonists, Colony.Colonists);
            ModifColony.Colonists = Colony.Colonists;

            return Page();
        }

        string newPicture;
        if (ModifColony.PictureTeam == Colony.LogoPath)
        {
            newPicture = Colony.LogoPath;
        }
        else
        {
            AnalyzeProfilePicture analyzeProfilePicture = new AnalyzeProfilePicture(ModifColony.Name);
            IDeletePicture deletePicture = new DeletePicture();
            deletePicture.DeleteImage(Colony.LogoPath, false);
            newPicture = analyzeProfilePicture.GetProfilePictureFileName(ModifColony.PictureTeam);
        }
        
        ColonyModel colonyModel = new ColonyModel()
        {
            Id = TeamId,
            Name = ModifColony.Name,
            OwnerId = TeamOwner.Id.ToString(),
            LogoPath = newPicture,
            Colonists = ModifColony.Colonists
        };
        
        await colonyRepository.UpdateColonyAsync(colonyModel);

        return RedirectToPage("/TeamProfile", new { teamId = TeamId });
    }
    
    private IList<ColonistModel> GetSelectedColonists(IList<Guid> colonistIds)
    {
        return Colonists
            .Where(c => colonistIds.Contains(Guid.Parse(c.Id)))
            .ToList();
    }
}