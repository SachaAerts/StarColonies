namespace StarColonies.Domains.Services.pictures;

public class AnalyzeItemPicture(string itemName, string uploadDir)
{
    public string SaveItemPicture(string picture)
    { 
        if (string.IsNullOrWhiteSpace(picture)) return "1.png";

        if (picture.StartsWith("/") || picture.StartsWith("http") || picture.StartsWith("https"))
        {
            var fileName = Path.GetFileName(picture);
            return string.IsNullOrWhiteSpace(fileName) ? "1.png" : fileName;
        }

        if (picture.StartsWith("data:image"))
        {
            var base64Data = picture[(picture.IndexOf(',') + 1)..];
            var bytes = Convert.FromBase64String(base64Data);
            
            var extension = GetImageExtension(picture);
            var fileName = GenerateUniqueFileName(itemName, extension);

            EnsureDirectoryExists(uploadDir);

            var fullPath = Path.Combine(uploadDir, fileName);

            while (File.Exists(fullPath))
            {
                fileName = GenerateUniqueFileName(itemName, extension, forceGuid: true);
                fullPath = Path.Combine(uploadDir, fileName);
            }

            File.WriteAllBytes(fullPath, bytes);

            return $"/img/upload/{fileName}";
        }

        return picture;
    }

    private void EnsureDirectoryExists(string directory)
    {
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
    }
    
    private string GenerateUniqueFileName(string baseName, string extension, bool forceGuid = false)
    {
        var sanitized = SanitizeFileName(baseName);
        if (forceGuid) return $"{sanitized}_{Guid.NewGuid()}{extension}";

        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return $"{sanitized}_{timestamp}{extension}";
    }

    private string GetImageExtension(string picture)
    {
        if (picture.StartsWith("data:image/jpeg")) return ".jpg";
        if (picture.StartsWith("data:image/png")) return ".png";
        return picture.StartsWith("data:image/gif") ? ".gif" : ".png";
    }

    private string SanitizeFileName(string input)
    {
        input = Path.GetInvalidFileNameChars().Aggregate(input, (current, c) => current.Replace(c, '_'));
        return input.Replace(" ", "_").ToLowerInvariant();
    }
}