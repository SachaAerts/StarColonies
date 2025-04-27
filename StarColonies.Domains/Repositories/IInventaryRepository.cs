using StarColonies.Domains.Models.Items;

namespace StarColonies.Domains.Repositories;

public interface IInventaryRepository
{
    Task<IList<RewardItemModel>> GetItemsForColonistAsync(string colonistId);
    Task AddItemToUser(string userId, RewardItemModel item);
    Task AddItemToUserFromShop(string userId, ItemModel item);
    Task SubstractItemToUserFromShop(string userId, ItemModel item);
    Task UseItemFromUserAsync(string userId, IList<ItemModel> items);
}