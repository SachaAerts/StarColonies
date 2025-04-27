using Microsoft.EntityFrameworkCore;
using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Repositories;
using StarColonies.Infrastructures.Data;

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
            NumberOfBuy = i.NumberOfBuy
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
}