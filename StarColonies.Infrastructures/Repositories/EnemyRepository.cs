using Microsoft.EntityFrameworkCore;
using StarColonies.Domains.Models.Missions;
using StarColonies.Domains.Repositories;
using StarColonies.Infrastructures.Data;

namespace StarColonies.Infrastructures.Repositories;

public class EnemyRepository(StarColoniesDbContext context) : IEnemyRepository
{
    public async Task<IList<EnemyModel>> GetAllEnemiesListAsync()
    {
        var enemies = await context.Enemy
            .Include(e => e.Type)
            .ToListAsync();

        return enemies.Select(e => new EnemyModel
        {
            Id = e.Id,
            Name = e.Name,
            Type = e.Type.Name,
            Strength = e.Strength,
            Stamina = e.Stamina,
            ImagePath = e.ImagePath
        }).ToList();
    }
}