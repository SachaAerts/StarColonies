using System.ComponentModel.DataAnnotations;

namespace StarColonies.Web.Validators;

public class TextAttribute : ValidationAttribute
{
    public int Max { get; set; } = 100;
    public int Min { get; set; } = 1;

    public override bool IsValid(object? value)
    {
        if (value is not string text) return true;

        if (text.Length >= Min && text.Length <= Max) return true;
        ErrorMessage = $"You can select between {Min} and {Max} characters.";
        return false;
    }
}