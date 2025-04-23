using StarColonies.Domains.Models.Items;

namespace StarColonies.Domains.Repositories;

public interface IInventaryRepository
{
    Task<IList<RewardItemModel>> GetItemsForColonistAsync(string colonistId);
    Task AddItemToUser(string userId, RewardItemModel item);
}