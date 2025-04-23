using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Models.Missions;
using StarColonies.Domains.Repositories;
using StarColonies.Infrastructures.Data;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Entities.Items;
using StarColonies.Infrastructures.Mapper.DomainToEntity;
using StarColonies.Infrastructures.Mapper.EntityToDomain;
using StarColonies.Infrastructures.Services;

namespace StarColonies.Infrastructures.Repositories;

public class RewardRepository(StarColoniesDbContext context,
    UserManager<ColonistEntity> userManager, 
    IInventaryRepository inventaryRepository,
    IDomainToEntityMapper<ColonistEntity, ColonistModel> mapper,
    IEntityToDomainMapper<ItemModel, ItemEntity> itemMapper,
    IColonyRepository colonyRepository) : IRewardRepository
{
    private RewardService RewardService => new(userManager, inventaryRepository, mapper);
    
    public async Task<IList<RewardItemModel>> GetRewardsForMissionAsync(int missionId)
    {
        var rewards = await context.Rewarded
            .Include(r => r.Item)
            .ThenInclude(i => i.Effect)
            .Where(r => r.MissionId == missionId)
            .ToListAsync();

        return rewards.Select(r 
            => new RewardItemModel { Item = itemMapper.Map(r.Item), Quantity = r.Quantity }).ToList();
    }
    
    public async Task GiveRewardAsync(ColonistModel userModel, MissionResultModel result, int colonyId)
    {
        ColonistEntity user = mapper.Map(userModel);
    
        if (result is { OvercomingMission: true, LivingColony: true })
        {
            var members = await colonyRepository.GetColonistsForColonyAsync(colonyId);
            
            await RewardService.GiveLevelsToMembersAsync(members);
            await RewardService.GiveResourcesToOwnerAsync(user, result);
            
            await userManager.UpdateAsync(user);
        }
    }
    
}
