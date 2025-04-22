using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Repositories;
using StarColonies.Domains.Services.pictures;
using StarColonies.Web.wwwroot.models;

namespace StarColonies.Web.Pages
{
    public class ModifyColon : PageModel
    {
        private readonly IColonistRepository _colonistRepository;

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public required ColonistModel Colonist { get; set; }

        [BindProperty]
        public ModifyProfileModel ModifyUser { get; set; } = new();
        
        public ModifyColon(IColonistRepository colonistRepository)
        {
            _colonistRepository = colonistRepository;
        }

        public async Task<IActionResult> OnGet()
        {
            if (!User.Identity?.IsAuthenticated ?? true)
                return Forbid();

            Colonist = await _colonistRepository.GetColonistByIdAsync(Id.ToString());

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            Colonist = await _colonistRepository.GetColonistByIdAsync(Id.ToString());

            if (!ModelState.IsValid)
                return Page();

            var analyzer = new AnalyzeProfilePicture(ModifyUser.SettlerName);

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
                ProfilPicture = analyzer.GetProfilePictureFileName(ModifyUser.ProfilePicture)
            };

            await _colonistRepository.UpdateColonistAsync(colonist);

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