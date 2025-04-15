using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StarColonies.Domains.Models;
using StarColonies.Domains.Models.Items;
using StarColonies.Infrastructures.Data.dataclass;

namespace StarColonies.Infrastructures.Data;

public class StarColoniesDbContext(DbContextOptions options) : IdentityDbContext<Colonist>(options)
{
    public DbSet<Colonist> Colonists { get; set; }
    public DbSet<MissionModel> Missions { get; set; }
    public DbSet<PlanetModel> Planets { get; set; }
    public DbSet<ItemModel> Items { get; set; }
    public DbSet<EnemyModel> Enemies { get; set; }

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