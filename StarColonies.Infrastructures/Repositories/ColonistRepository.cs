using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Repositories;
using StarColonies.Infrastructures.Data;
using StarColonies.Infrastructures.Services.Repositories.AddingDataToDB;
using StarColonies.Infrastructures.Services.Repositories.DeletingDataToDB;
using StarColonies.Infrastructures.Services.Repositories.GettingDataToDB;
using StarColonies.Infrastructures.Services.Repositories.UpdateDataToDB;

namespace StarColonies.Infrastructures.Repositories;

public class ColonistRepository(
    IGetting<ColonistModel> getting,
    IAdding<ColonistModel> adding,
    IUpdate<ColonistModel> update,
    IDeleting<ColonistModel> deleting) : IColonistRepository 
{
    public async Task AddColonistAsync(ColonistModel colonist)
        => await adding.AddAsync(colonist);
 
    public async Task<IList<ColonistModel>> GetColonistsAsync()
        => await getting.GetIListDataAsync();

    public async Task<ColonistModel> GetColonistByIdAsync(string id)
        => await getting.GetEntityByIdAsync(id);

    public async Task UpdateColonistAsync(ColonistModel colonistModel)
        => await update.UpdateAsync(colonistModel);

    public async Task DeleteColonistAsync(string id)
        => await deleting.DeleteEntityAsync(id, null);
}