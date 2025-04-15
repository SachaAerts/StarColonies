using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Domains.Models;

namespace StarColonies.Infrastructures.Data.Configurations;

public class PlanetConfiguration : IEntityTypeConfiguration<PlanetModel>
{
    public void Configure(EntityTypeBuilder<PlanetModel> builder)
    {
        builder.HasKey(m => m.Id);
        
        builder.Property(m => m.Name)
               .IsRequired()
               .HasMaxLength(100);
        
        builder.HasIndex(m => m.Name)
               .IsUnique();
        
        builder.Property(m => m.ImagePath)
               .IsRequired();

        builder.HasMany(m => m.Missions)
               .WithOne(p => p.Planet)
               .HasForeignKey(m => m.PlanetId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}