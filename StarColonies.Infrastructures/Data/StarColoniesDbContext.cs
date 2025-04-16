using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StarColonies.Infrastructures.Data.dataclass;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Entities.Items;
using StarColonies.Infrastructures.Data.Entities.Missions;

namespace StarColonies.Infrastructures.Data;

public class StarColoniesDbContext(DbContextOptions options) : IdentityDbContext<Colonist>(options)
{
    public DbSet<Colonist> Colonists { get; set; }
    public DbSet<MissionEntity> Missions { get; set; }
    public DbSet<PlanetEntity> Planets { get; set; }
    public DbSet<ItemEntity> Items { get; set; }
    public DbSet<EnemyEntity> Enemies { get; set; }
    public DbSet<EffectEntity> Effects { get; set; }
    public DbSet<RewardedEntity> Rewarded { get; set; }
    public DbSet<TypeEntity> Types { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StarColoniesDbContext).Assembly);

        // Conversion de l'enum Job en texte dans la base de données
        modelBuilder
            .Entity<Colonist>()
            .Property(c => c.JobModel)
            .HasConversion<string>();
    }
}