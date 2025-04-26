using StarColonies.Domains.Models.Items;

namespace StarColonies.Domains.Repositories;

public interface IItemRepository
{
    Task<IList<ItemModel>> GetAllItemsAsync();
    Task<ItemModel?> GetItemByIdAsync(int id);
}