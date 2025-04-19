using StarColonies.Domains.Models.Items;

namespace StarColonies.Domains.Repositories;

public interface IInventaryRepository
{
    Task<IList<ItemModel>> GetItemsForColonistAsync(string colonistId);
}