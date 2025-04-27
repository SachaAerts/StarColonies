using StarColonies.Domains.Models;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Infrastructures.Data.Seeder.Factories;

public class ColonistFactory
{
    public static ColonistEntity Create(string userName, string email, string username, DateTime dateOfBirth, int level, int strength, int stamina, int musty, string profilePicture, JobModel jobModel, bool emailConfirmed)
        => new()
        {
            UserName = userName,
            Email = email,
            NormalizedEmail = email.ToUpperInvariant(),
            NormalizedUserName = username.ToUpperInvariant(),
            DateOfBirth = dateOfBirth,
            Level = level,
            Strength = strength,
            Stamina = stamina,
            Musty = musty,
            ProfilPicture = profilePicture,
            JobModel = jobModel,
            EmailConfirmed = emailConfirmed
        };
}