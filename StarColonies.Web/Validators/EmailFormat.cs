using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace StarColonies.Web.Validators;

public class EmailFormat: ValidationAttribute
{
    private const string Pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string email || string.IsNullOrWhiteSpace(email))
        {
            return new ValidationResult("Email address is required.");
        }

        if (!Regex.IsMatch(email, Pattern))
        {
            return new ValidationResult("Invalid email format.");
        }

        return ValidationResult.Success;
    }
}