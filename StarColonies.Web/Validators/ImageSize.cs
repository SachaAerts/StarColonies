using System.ComponentModel.DataAnnotations;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;

namespace StarColonies.Web.Validators;

public class ImageSize(int maxWidth = 320, int maxHeight = 320) : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string imagePath || string.IsNullOrWhiteSpace(imagePath))
            return ValidationResult.Success;

        try
        {
            if (imagePath.StartsWith("data:image"))
            {
                var base64Data = imagePath.Substring(imagePath.IndexOf(',') + 1);
                var bytes = Convert.FromBase64String(base64Data);

                using var ms = new MemoryStream(bytes);
                using var image = Image.Load(new DecoderOptions(), ms);

                if (image.Width > maxWidth || image.Height > maxHeight)
                {
                    return new ValidationResult($"The uploaded image must not exceed {maxWidth}x{maxHeight} pixels. (Current: {image.Width}x{image.Height})");
                }
            }
            else
            {
                var relativePath = imagePath.TrimStart('/');
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "upload", Path.GetFileName(relativePath));

                if (!File.Exists(fullPath))
                    return new ValidationResult("The selected image file does not exist on the server.");

                using var image = Image.Load(new DecoderOptions(), fullPath);
                if (image.Width > maxWidth || image.Height > maxHeight)
                {
                    return new ValidationResult($"The selected image must not exceed {maxWidth}x{maxHeight} pixels. (Current: {image.Width}x{image.Height})");
                }
            }
        }
        catch
        {
            return new ValidationResult("Invalid or unreadable image file.");
        }

        return ValidationResult.Success;
    }
}