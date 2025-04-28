using Microsoft.EntityFrameworkCore;
using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Repositories;
using StarColonies.Infrastructures.Data;
using StarColonies.Infrastructures.Data.Entities.Items;

namespace StarColonies.Infrastructures.Repositories;

public class ItemRepository(StarColoniesDbContext context) : IItemRepository
{
    public async Task<IList<ItemModel>> GetAllItemsAsync()
    {
        var items = await context.Item
            .Include(i => i.Effect)
            .ToListAsync();

        return items.Select(i => new ItemModel()
        {
            Id = i.Id,
            Name = i.Name,
            ImagePath = i.ImagePath,
            CoinsValue = i.CoinsValue,
            Description = i.Description,
            Effect = new EffectModel()
            {
                Id = i.Effect.Id,
                ForceModifier = i.Effect.ForceModifier,
                StaminaModifier = i.Effect.StaminaModifier,
            },
            NumberOfBuy = i.NumberOfBuy,
            IsLegendary = i.isLegendary
        }).ToList();
    }

    public async Task<ItemModel?> GetItemByIdAsync(int id)
        => await context.Item
            .Include(i => i.Effect)
            .Select(i => new ItemModel()
            {
                Id = i.Id,
                Name = i.Name,
                ImagePath = i.ImagePath,
                CoinsValue = i.CoinsValue,
                Description = i.Description,
                Effect = new EffectModel()
                {
                    Id = i.Effect.Id,
                    ForceModifier = i.Effect.ForceModifier,
                    StaminaModifier = i.Effect.StaminaModifier,
                }
            })
            .FirstOrDefaultAsync(i => i.Id == id);
    
    public async Task CreateItemAsync(ItemModel itemModel, EffectModel effectModel)
    {
        var effectEntity = new EffectEntity
        {
            Name = $"{itemModel.Name} Effect",
            ForceModifier = effectModel.ForceModifier,
            StaminaModifier = effectModel.StaminaModifier
        };

        await context.Effect.AddAsync(effectEntity);
        await context.SaveChangesAsync();

        var itemEntity = new ItemEntity
        {
            Name = itemModel.Name,
            ImagePath = itemModel.ImagePath,
            CoinsValue = itemModel.CoinsValue,
            Description = itemModel.Description,
            isLegendary = itemModel.IsLegendary,
            NumberOfBuy = itemModel.NumberOfBuy,
            EffectId = effectEntity.Id
        };

        await context.Item.AddAsync(itemEntity);
        await context.SaveChangesAsync();
    }
    
    public async Task UpdateItemAsync(ItemModel updatedItem, EffectModel updatedEffect)
    {
        var itemEntity = await context.Item
            .Include(i => i.Effect)
            .FirstOrDefaultAsync(i => i.Id == updatedItem.Id);

        if (itemEntity == null)
            throw new InvalidOperationException($"Item with ID {updatedItem.Id} not found.");

        itemEntity.Name = updatedItem.Name;
        itemEntity.ImagePath = updatedItem.ImagePath;
        itemEntity.CoinsValue = updatedItem.CoinsValue;
        itemEntity.Description = updatedItem.Description;
        itemEntity.isLegendary = updatedItem.IsLegendary;
        
        if (itemEntity.Effect != null)
        {
            itemEntity.Effect.ForceModifier = updatedEffect.ForceModifier;
            itemEntity.Effect.StaminaModifier = updatedEffect.StaminaModifier;
        }

        await context.SaveChangesAsync();
    }
    
    public async Task DeleteItemAsync(int itemId)
    {
        var inventories = await context.Inventory
            .Where(inv => inv.ItemId == itemId)
            .ToListAsync();

        if (inventories.Any())
        {
            context.Inventory.RemoveRange(inventories);
        }

        var itemEntity = await context.Item
            .FirstOrDefaultAsync(i => i.Id == itemId);

        if (itemEntity == null)
            throw new InvalidOperationException($"Item with ID {itemId} not found.");

        context.Item.Remove(itemEntity);

        await context.SaveChangesAsync();
    }
}