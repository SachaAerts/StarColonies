using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace StarColonies.Web.Validators;

public class DateFormat(string expectedFormat) : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null) return true;

        var stringValue = value.ToString();

        bool isValid = DateTime.TryParseExact(
            stringValue,
            expectedFormat,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out _);

        if (!isValid)
        {
            ErrorMessage ??= $"Invalid date format. Expected: {expectedFormat}.";
        }

        return isValid;
    }
}