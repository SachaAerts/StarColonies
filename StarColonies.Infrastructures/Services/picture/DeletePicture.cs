using StarColonies.Domains.Services.pictures;

namespace StarColonies.Infrastructures.Services.picture;

public class DeletePicture : IDeletePicture
{
    private static readonly List<string> DefaultImages =
    [
        "default_team_logo.png",
        "team1.png", "1.png",
        "team2.png", "2.png",
        "team3.png", "3.png",
        "team4.png", "4.png",
        "team5.png", "5.png",
        "team6.png", "6.png",
        "team7.png", "7.png",
        "team8.png", "8.png",
        "team9.png", "9.png",
        "team10.png", "10.png",
        "team11.png", "11.png",
        "team12.png", "12.png"
    ];

    private bool IsDefaultImage(string fileName)
    {
        return DefaultImages.Contains(fileName);
    }

    public void DeleteImage(string fileName)
    {
        if (IsDefaultImage(fileName))
            return;

        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "upload", fileName);

        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
    }
}