namespace StarColonies.Domains.Services.pictures;

public class AnalyzeItemPicture(string itemName, string uploadDir)
{
    public string SaveItemPicture(string base64Image)
    {
        if (string.IsNullOrWhiteSpace(base64Image)) throw new ArgumentException("Base64 image data is required.");

        var base64Data = base64Image.Substring(base64Image.IndexOf(',') + 1);
        var bytes = Convert.FromBase64String(base64Data);

        var extension = GetImageExtension(base64Image);
        var fileName = GenerateUniqueFileName(itemName, extension);

        if (!Directory.Exists(uploadDir)) Directory.CreateDirectory(uploadDir);

        var fullPath = Path.Combine(uploadDir, fileName);

        while (File.Exists(fullPath))
        {
            fileName = GenerateUniqueFileName(itemName, extension, forceGuid: true);
            fullPath = Path.Combine(uploadDir, fileName);
        }

        File.WriteAllBytes(fullPath, bytes);

        return fileName;
    }

    private string GenerateUniqueFileName(string baseName, string extension, bool forceGuid = false)
    {
        var sanitized = SanitizeFileName(baseName);
        if (forceGuid) return $"{sanitized}_{Guid.NewGuid()}{extension}";

        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return $"{sanitized}_{timestamp}{extension}";
    }

    private string GetImageExtension(string base64Image)
    {
        if (base64Image.StartsWith("data:image/jpeg")) return ".jpg";
        if (base64Image.StartsWith("data:image/png")) return ".png";
        return base64Image.StartsWith("data:image/gif") ? ".gif" : ".png";
    }

    private string SanitizeFileName(string input)
    {
        input = Path.GetInvalidFileNameChars().Aggregate(input, (current, c) => current.Replace(c, '_'));
        return input.Replace(" ", "_").ToLowerInvariant();
    }
}