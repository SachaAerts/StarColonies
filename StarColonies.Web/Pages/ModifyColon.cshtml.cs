using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Repositories;
using StarColonies.Domains.Services.pictures;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Services.picture;
using StarColonies.Web.wwwroot.models;

namespace StarColonies.Web.Pages
{
    [Authorize]
    public class ModifyColon(UserManager<ColonistEntity> userManager, IColonistRepository colonistRepository) : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public required ColonistModel Colonist { get; set; }

        [BindProperty]
        public ModifyProfileModel ModifyUser { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.Identity?.IsAuthenticated ?? true)
                return Forbid();
            
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user!.Id != Id.ToString())
            {
                return RedirectToPage("Index");
            }

            Colonist = await colonistRepository.GetColonistByIdAsync(Id.ToString());

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Colonist = await colonistRepository.GetColonistByIdAsync(Id.ToString());

            if (!ModelState.IsValid)
                return Page();

            string newPicture;
            if (ModifyUser.ProfilePicture == Colonist.ProfilPicture)
            {
                newPicture = Colonist.ProfilPicture;
            }
            else
            {
                var analyzer = new AnalyzeProfilePicture(ModifyUser.SettlerName);
                IDeletePicture deletePicture = new DeletePicture();
                deletePicture.DeleteImage(Colonist.ProfilPicture, false);
                newPicture = analyzer.GetProfilePictureFileName(ModifyUser.ProfilePicture);
            }
            
            var colonist = new ColonistModel
            {
                Id = Colonist.Id,
                Name = ModifyUser.SettlerName,
                Email = ModifyUser.Email,
                DateOfBirth = DateTime.ParseExact(ModifyUser.BirthdayEntry, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Job = Enum.Parse<JobModel>(ModifyUser.Profession),
                Level = Colonist.Level,
                Strength = GetStrength(ModifyUser.Statistics),
                Stamina = GetStamina(ModifyUser.Statistics),
                Musty = Colonist.Musty,
                ProfilPicture = newPicture
            };

            await colonistRepository.UpdateColonistAsync(colonist);

            return RedirectToPage("/Profile", new { id = colonist.Id });
        }
        
        private int GetStrength(string stats) =>
            string.IsNullOrWhiteSpace(stats) ? -1 :
            stats.Split('-') is [var s, _] && int.TryParse(s, out var strength) ? strength : -1;

        private int GetStamina(string stats) =>
            string.IsNullOrWhiteSpace(stats) ? -1 :
            stats.Split('-') is [_, var s] && int.TryParse(s, out var stamina) ? stamina : -1;
    }
}