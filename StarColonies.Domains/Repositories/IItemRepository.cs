using StarColonies.Domains.Models.Items;

namespace StarColonies.Domains.Repositories;

public interface IItemRepository
{
    Task<IList<ItemModel>> GetAllItemsAsync();
    Task<ItemModel?> GetItemByIdAsync(int id);

    Task CreateItemAsync(ItemModel itemModel, EffectModel effectModel);

    Task UpdateItemAsync(ItemModel updatedItem, EffectModel updatedEffect);

    Task DeleteItemAsync(int itemId);
}