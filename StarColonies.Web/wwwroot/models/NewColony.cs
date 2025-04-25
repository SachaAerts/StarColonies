using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using StarColonies.Domains.Models.Colony;
using StarColonies.Web.validators.teamValidators;
using StarColonies.Web.Validators.TeamValidators;

namespace StarColonies.Web.wwwroot.models;

public class NewColony
{
    [Required(ErrorMessage = "Name required")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Picture required")]
    public string PictureTeam { get; set; }
    
    [NotMapped]
    [NumberOfColonist(ErrorMessage = "Team required 4 or 5 members")]
    [SameProfessionLimit(ErrorMessage = "No more than 2 times the same job")]
    [JobRequired(ErrorMessage = "Your team must include at least one: Doctor, Engineer and Scientist.")]
    public IList<ColonistModel> Colonists { get; set; } = new List<ColonistModel>();
    
    [Required(ErrorMessage = "Please select members for your team")]
    public string ColonistsJson { get; set; }

    [NotMapped]
    public List<Guid> ColonistsId =>
        string.IsNullOrWhiteSpace(ColonistsJson)
            ? new()
            : JsonSerializer.Deserialize<List<Guid>>(ColonistsJson)!;
}