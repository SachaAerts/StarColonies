using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Domains.Models;

namespace StarColonies.Infrastructures.Data.Configurations;

public class PlanetConfiguration : IEntityTypeConfiguration<PlanetModel>
{
    public void Configure(EntityTypeBuilder<PlanetModel> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.ImagePath).IsRequired();
        builder.Property(p => p.Missions).IsRequired();
        builder.Property(p => p.).HasMaxLength(500);
        
        builder.HasMany(p => p.Missions)
            .WithOne(m => m.Planet)
            .HasForeignKey(m => m.PlanetId);
    }
}