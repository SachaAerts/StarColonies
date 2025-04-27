using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Repositories;
using StarColonies.Infrastructures.Data;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Entities.Items;
using StarColonies.Infrastructures.Mapper;
using StarColonies.Infrastructures.Mapper.DomainToEntity;
using StarColonies.Infrastructures.Mapper.EntityToDomain;

namespace StarColonies.Infrastructures.Repositories;

public class InventaryRepository(
    IEntityToDomainMapper<ItemModel, ItemEntity> itemMapper, 
    IDomainToEntityMapper<ItemEntity, ItemModel?> reverseMapper,
    StarColoniesDbContext context) : IInventaryRepository
{
    
    public async Task<IList<RewardItemModel>> GetItemsForColonistAsync(string colonistId)
    {
        var items = await context.Inventory
            .Include(i => i.Item).ThenInclude(itemEntity => itemEntity.Effect)
            .Where(i => i.ColonistId == colonistId)
            .ToListAsync();
        return items 
            .Select(i => new RewardItemModel
            {
                Item = itemMapper.Map(i.Item),
                Quantity = i.Quantity
            })
            .ToList();
    }
    
    public async Task AddItemToUser(string userId, RewardItemModel reward)
    {
        if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));
        if (reward == null) throw new ArgumentNullException(nameof(reward), "Reward item cannot be null.");

        var itemEntity = reverseMapper.Map(reward.Item);

        var inventory = await context.Inventory
            .FirstOrDefaultAsync(i => i.ColonistId == userId && i.ItemId == itemEntity.Id);

        if (inventory != null) inventory.Quantity += reward.Quantity;
        else context.Inventory.Add(new InventoryEntity { ColonistId = userId, ItemId = itemEntity.Id, Quantity = reward.Quantity });

        await context.SaveChangesAsync();
    }
    
    public async Task UseItemFromUserAsync(string userId, List<ItemModel?> items)
    {
        if (string.IsNullOrWhiteSpace(userId)) 
            throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));
        
        IList<InventoryEntity> inventory = await context.Inventory
            .Where(i => i.ColonistId == userId && GetItemsIdsList(items).Contains(i.ItemId))
            .ToListAsync() ?? throw new InvalidOperationException("Item not found in inventory.");

        foreach (var item in items) 
            HandleItem(inventory, item);

        await context.SaveChangesAsync();
    }
    
    private IList<int> GetItemsIdsList(IList<ItemModel> items)
        => items.Select(i => i.Id).ToList();

    private void HandleItem(IList<InventoryEntity> inventory, ItemModel item)
    {
        var inventoryItem = inventory.FirstOrDefault(i => i.ItemId == item.Id) 
                            ?? throw new InvalidOperationException($"Item '{item.Name}' (ID: {item.Id}) not found in inventory.");
        if (inventoryItem.Quantity > 1) inventoryItem.Quantity -= 1;
            else context.Inventory.Remove(inventoryItem);
    }
}