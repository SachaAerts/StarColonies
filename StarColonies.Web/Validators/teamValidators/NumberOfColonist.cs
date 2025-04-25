using System.ComponentModel.DataAnnotations;
using StarColonies.Domains.Models.Colony;

namespace StarColonies.Web.validators.teamValidators;

public class NumberOfColonist: ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is IList<ColonistModel> list)
        {
            return list.Count is 4 or 5;
        }

        return false;
    }
}