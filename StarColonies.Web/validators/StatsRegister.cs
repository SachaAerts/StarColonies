using System.ComponentModel.DataAnnotations;

namespace StarColonies.Web.validators;

public class StatsRegister : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is not string input || string.IsNullOrWhiteSpace(input))
            return false;

        var parts = input.Split('-');
        if (parts.Length != 2) return false;

        if (int.TryParse(parts[0], out int left) && int.TryParse(parts[1], out int right))
        {
            return left + right == 7;
        }

        return false;
    }
}