using Microsoft.EntityFrameworkCore;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Repositories;
using StarColonies.Infrastructures.Data;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Mapper.DomainToEntity;
using StarColonies.Infrastructures.Mapper.EntityToDomain;

namespace StarColonies.Infrastructures.Repositories;

public class ColonistRepository(StarColoniesDbContext context,
    IEntityToDomainMapper<ColonistModel, ColonistEntity> colonistMapper,
    IDomainToEntityMapper<ColonistEntity, ColonistModel> colonistReverseMapper)
    : IColonistRepository 
{
    public async Task<IList<ColonistModel>> GetColonistsAsync()
    {
        var colonists = await context.Users.ToListAsync();
        return colonists.Select(colonistMapper.Map).ToList();
    }

    public async Task<ColonistModel> GetColonistByIdAsync(string id)
    {
        var entity = await context.Users.FindAsync(id);
        return entity != null ? colonistMapper.Map(entity) : null!;
    }

    public async Task<ColonistModel> GetColonistByNameAsync(string name)
    {
        var entity = await context.Users.FirstOrDefaultAsync(c => c.UserName == name);
        return entity != null ? colonistMapper.Map(entity) : null!;
    }

    public async Task AddColonistAsync(ColonistModel colonist)
    {
        var entity = colonistReverseMapper.Map(colonist);
        context.Users.Add(entity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateColonistAsync(ColonistModel colonistModel)
    {
        var entity = await context.Users.FindAsync(colonistModel.Id);

        if (entity == null) return;

        entity.UserName = colonistModel.Name;
        entity.Email = colonistModel.Email;
        entity.NormalizedEmail = colonistModel.Email?.ToUpperInvariant();
        entity.NormalizedUserName = colonistModel.Name?.ToUpperInvariant();
        entity.DateOfBirth = colonistModel.DateOfBirth;
        entity.JobModel = colonistModel.Job;
        entity.Level = colonistModel.Level;
        entity.Strength = colonistModel.Strength;
        entity.Stamina = colonistModel.Stamina;
        entity.Musty = colonistModel.Musty;
        entity.ProfilPicture = colonistModel.ProfilPicture;

        await context.SaveChangesAsync();
    }

    public async Task DeleteColonistAsync(string id)
    {
        var entity = await context.Users.FindAsync(id);
        if (entity == null) return;

        context.Users.Remove(entity);
        await context.SaveChangesAsync();
    }
}