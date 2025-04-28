using System.ComponentModel.DataAnnotations;

namespace StarColonies.Web.Validators;

public class RangeNumber(int min, int max, string input) : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is int intValue)
        {
            if (intValue > min && intValue < max)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage ?? $"{input} must be greater than {min} and less than {max}.");
        }

        return new ValidationResult("Invalid value type.");
    }
}