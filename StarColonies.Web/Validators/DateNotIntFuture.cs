using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace StarColonies.Web.validators;

public class DateNotIntFuture : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null)
            return true;

        var stringValue = value.ToString();

        if (DateTime.TryParseExact(
                stringValue,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var parsedDate))
        {
            return parsedDate.Date <= DateTime.Today;
        }

        return false;
    }
}