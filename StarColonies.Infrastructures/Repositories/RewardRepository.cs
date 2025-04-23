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
using StarColonies.Infrastructures.Services.RewardStrategy;

namespace StarColonies.Infrastructures.Repositories;

public class RewardRepository(StarColoniesDbContext context,
    UserManager<ColonistEntity> userManager, 
    IDomainToEntityMapper<ColonistEntity, ColonistModel> mapper,
    IEntityToDomainMapper<ItemModel, ItemEntity> itemMapper,
    IStrategyFactory missionRewardStrategyFactory) : IRewardRepository
{
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
        var user = await context.Users.FindAsync(userModel.Id);
        
        IMissionRewardStrategy strategy = missionRewardStrategyFactory.GetStrategy(result) 
                                          ?? throw new InvalidOperationException("No strategy found for the given result.");

        await strategy.ExecuteAsync(user, result, colonyId);
        await userManager.UpdateAsync(user);
    }
    
}
