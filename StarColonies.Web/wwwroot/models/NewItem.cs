using System.ComponentModel.DataAnnotations;
using StarColonies.Web.Validators;

namespace StarColonies.Web.wwwroot.models;

public class NewItem
{
    [Required(ErrorMessage = "Name is required")]
    public required string NameItem { get; set; }
    
    [Required(ErrorMessage = "Picture is required")]
    public required string Picture { get; set; }

    [Required(ErrorMessage = "Strength is required")]
    [RangeNumber(-1, 100, "Strength")]
    public required int ForceModifier { get; set; }
    
    [Required(ErrorMessage = "Stamina is required")]
    [RangeNumber(-1, 100, "Stamina")]
    public required int StaminaModifier { get; set; }
    
    [Required(ErrorMessage = "Price is required")]
    [RangeNumber(-1, 1000, "Price")]
    public required int Price { get; set; }
    
    public required bool IsLegendary { get; set; }
}