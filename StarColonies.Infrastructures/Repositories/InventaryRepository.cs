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
    IDomainToEntityMapper<ItemEntity, ItemModel> reverseMapper,
    StarColoniesDbContext context) : IInventaryRepository
{
    
    public async Task<IList<ItemModel>> GetItemsForColonistAsync(string colonistId)
    {
        var items = await context.Inventory
            .Where(i => i.ColonistId == colonistId)
            .Include(i => i.Item)
            .ThenInclude(item => item.Effect)
            .Select(i => i.Item)
            .ToListAsync();

        return items.Select(itemMapper.Map).ToList();
    }
    
    public async Task AddItemToUser(string userId, ItemModel item)
    {
        var itemEntity = reverseMapper.Map(item);
        
        var existingInventory = await context.Inventory
            .FirstOrDefaultAsync(i => i.ColonistId == userId && i.ItemId == itemEntity.Id);

        if (existingInventory != null) existingInventory.Quantity += 1;
        else
        {
            context.Inventory.Add(new InventoryEntity
            {
                ColonistId = userId,
                ItemId = itemEntity.Id,
                Quantity = 1
            });
        }
        
        await context.SaveChangesAsync();
    }
    
    public async Task UseItemFromUserAsync(string userId, int itemId)
    {
        var inventory = await context.Inventory
            .FirstOrDefaultAsync(i => i.ColonistId == userId && i.ItemId == itemId);

        if (inventory == null) throw new InvalidOperationException("Item not found in inventory.");

        if (inventory.Quantity > 1) inventory.Quantity -= 1;
            else context.Inventory.Remove(inventory);

        await context.SaveChangesAsync();
    }
}