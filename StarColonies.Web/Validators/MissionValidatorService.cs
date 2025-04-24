using System.ComponentModel.DataAnnotations;

namespace StarColonies.Web.Validators;

public class MissionValidatorService : ValidationAttribute
{
    public int MaxEnemies { get; set; } = 3;
    public bool RequireEnemies { get; set; } = false;
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not List<int> selectedIds) return ValidationResult.Success;
        return selectedIds.Count > MaxEnemies 
            ? new ValidationResult($"Vous pouvez sélectionner au maximum {MaxEnemies} ennemis.") 
            : ValidationResult.Success;
    }
}