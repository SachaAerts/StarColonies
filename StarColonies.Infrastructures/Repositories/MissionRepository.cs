using Microsoft.EntityFrameworkCore;
using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Models.Missions;
using StarColonies.Domains.Repositories;
using StarColonies.Infrastructures.Data;
using StarColonies.Infrastructures.Data.Entities.Items;
using StarColonies.Infrastructures.Data.Entities.Missions;
using StarColonies.Infrastructures.Mapper.DomainToEntity;
using StarColonies.Infrastructures.Mapper.EntityToDomain;

namespace StarColonies.Infrastructures.Repositories;

public class MissionRepository(
    StarColoniesDbContext context, 
    IDomainToEntityMapper<ItemEntity, ItemModel> itemMapper,
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

    public async Task MissionExecute(int id, int colonyId, MissionResultModel result)
    {
        var mission = await context.Mission
            .Include(m => m.Rewards)
            .ThenInclude(r => r.Item)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (mission == null) throw new InvalidOperationException($"Mission with id {id} not found");

        var missionExecution = new MissionExecutionEntity
        {
            ColonyId = colonyId,
            MissionId = id,
            LivingColony = result.LivingColony,
            OvercomingMission = result.OvercomingMission,
            IsSuccess = result.MissionSuccess,
            RewardedCoins = result.CoinsReward
        };

        await context.MissionExecution.AddAsync(missionExecution);
        await context.SaveChangesAsync();
    }

    public async Task CreateMissionAsync(int planetId, MissionModel mission, IList<int> selectedEnemyIds, IList<RewardItemModel> rewardItems)
    {
        var enemies = await context.Enemy
            .Where(e => selectedEnemyIds.Contains(e.Id))
            .ToListAsync();

        var newMission = new MissionEntity
        {
            PlanetId = planetId,
            Name = mission.Name,
            Description = mission.Description,
            CoinsReward = mission.CoinsReward,
            Enemies = enemies,
            Difficulty = enemies.Sum(e => e.Strength)
        };
        
        newMission.Rewards = rewardItems.Select(r => new RewardedEntity()
        {
            MissionId = newMission.Id,
            Mission = newMission,
            ItemId = r.Item.Id,
            Quantity = r.Quantity
        }).ToList();

        await context.Mission.AddAsync(newMission);
        await context.SaveChangesAsync();
    }

    public async Task UpdateMissionAsync(MissionModel updatedModel, IList<int> selectedEnemyIds, IList<RewardItemModel> rewardItems)
    {
        var existingMission = await context.Mission
            .Include(m => m.Enemies)
            .Include(m => m.Rewards)
            .ThenInclude(r => r.Item)
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
        
        context.Rewarded.RemoveRange(existingMission.Rewards);

        existingMission.Rewards = rewardItems.Select(r => new RewardedEntity()
        {
            MissionId = existingMission.Id,
            Mission = existingMission,
            ItemId = r.Item.Id,
            Quantity = r.Quantity
        }).ToList();

        await context.SaveChangesAsync();
    }
    
    public async Task VisibleMissionAsync(int missionId)
    {
        var mission = await context.Mission.FindAsync(missionId);
        if (mission != null)
        {
            mission.Visible = !mission.Visible;
            context.Mission.Update(mission);
            await context.SaveChangesAsync();
        }
    }
    
    public async Task<IList<MissionExecutedModel>> GetAllMissionExecutionsAsync()
    {
        var executions = await context.MissionExecution
            .Include(me => me.Mission)
            .ThenInclude(m => m.Planet)
            .Select(me => new MissionExecutedModel
            {
                IsSuccess = me.IsSuccess,
                PlanetId = me.Mission.PlanetId
            })
            .ToListAsync();

        return executions;
    }
}