namespace StarColonies.Domains.Services.pictures;

public class AnalyzeProfilePicture(string settlerName)
{
    private readonly string _settlerName = settlerName;

    public string GetProfilePictureFileName(string picture)
    {
        if (string.IsNullOrWhiteSpace(picture))
            return "1.png";

        // Cas 1 : URL ou chemin déjà défini
        if (picture.StartsWith("/") || picture.StartsWith("http"))
        {
            var fileName = Path.GetFileName(picture);
            return string.IsNullOrWhiteSpace(fileName) ? "1.png" : fileName;
        }

        // Cas 2 : base64
        if (picture.StartsWith("data:image"))
        {
            var base64Data = picture.Substring(picture.IndexOf(',') + 1);
            var bytes = Convert.FromBase64String(base64Data);

            var extension = GetImageExtension(picture);
            var fileName = GenerateUniqueFileName(_settlerName, extension);
            var uploadDir = Path.Combine("wwwroot", "img", "upload");

            if (!Directory.Exists(uploadDir))
                Directory.CreateDirectory(uploadDir);

            var fullPath = Path.Combine(uploadDir, fileName);
            
            while (File.Exists(fullPath))
            {
                fileName = GenerateUniqueFileName(_settlerName, extension, forceGuid: true);
                fullPath = Path.Combine(uploadDir, fileName);
            }

            File.WriteAllBytes(fullPath, bytes);
            return fileName;
        }

        return "1.png";
    }
    
    private string GenerateUniqueFileName(string baseName, string extension, bool forceGuid = false)
    {
        var sanitized = SanitizeFileName(baseName);
        if (forceGuid)
        {
            return $"{sanitized}_{Guid.NewGuid()}{extension}";
        }

        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return $"{sanitized}_{timestamp}{extension}";
    }

    private string GetImageExtension(string picture)
    {
        if (picture.StartsWith("data:image/jpeg")) return ".jpg";
        if (picture.StartsWith("data:image/png")) return ".png";
        if (picture.StartsWith("data:image/gif")) return ".gif";
        return ".png";
    }

    private string SanitizeFileName(string input)
    {
        foreach (var c in Path.GetInvalidFileNameChars())
        {
            input = input.Replace(c, '_');
        }
        return input.Replace(" ", "_").ToLowerInvariant();
    }
    
    public string GetProfilePictureFileNameForUpdate(string newPicture, string currentFileName)
    {
        if (string.IsNullOrWhiteSpace(newPicture))
            return currentFileName;

        return GetProfilePictureFileName(newPicture);
    }
}