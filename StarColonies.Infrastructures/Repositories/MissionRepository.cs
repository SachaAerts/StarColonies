using Microsoft.EntityFrameworkCore;
using StarColonies.Domains.Models.Missions;
using StarColonies.Domains.Repositories;
using StarColonies.Infrastructures.Data;
using StarColonies.Infrastructures.Data.Entities.Missions;
using StarColonies.Infrastructures.Mapper.EntityToDomain;

namespace StarColonies.Infrastructures.Repositories;

public class MissionRepository(
    StarColoniesDbContext context,
    IEntityToDomainMapper<MissionModel, MissionEntity> missionMapper
    ) : IMissionRepository
{
    public async Task<MissionModel> GetMissionByIdAsync(int id)
    {
        var mission = await context.Mission
            .Include(m => m.Enemies)
            .ThenInclude(e => e.Type)
            .Include(m => m.Rewards)
            .ThenInclude(r => r.Item)
            .ThenInclude(i => i.Effect)
            .FirstOrDefaultAsync(m => m.Id == id);

        return missionMapper.Map(mission ?? throw new InvalidOperationException($"Mission with id {id} not found"));
    }

    public async Task UpdateMissionAsync(MissionModel updatedModel, IList<int> selectedEnemyIds)
    {
        var existingMission = await context.Mission
            .Include(m => m.Enemies)
            .Include(m => m.Rewards)
            .FirstOrDefaultAsync(m => m.Id == updatedModel.Id);

        if (existingMission == null)
            throw new InvalidOperationException($"Mission with id {updatedModel.Id} not found");

        existingMission.Name = updatedModel.Name;
        existingMission.Description = updatedModel.Description;
        existingMission.CoinsReward = updatedModel.CoinsReward;

        var enemies = await context.Enemy
            .Where(e => selectedEnemyIds.Contains(e.Id))
            .ToListAsync();

        existingMission.Enemies.Clear();

        int difficulty = 0;
        foreach (var enemy in enemies)
        {
            difficulty += enemy.Strength;
            existingMission.Enemies.Add(enemy);
        }
        
        existingMission.Difficulty = difficulty;

        await context.SaveChangesAsync();
    }

    
}