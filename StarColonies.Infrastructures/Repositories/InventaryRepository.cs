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
        
        context.Inventory.Add(new InventoryEntity
        {
            ColonistId = userId,
            Item = itemEntity
        });
        
        context.SaveChanges();
    }
}