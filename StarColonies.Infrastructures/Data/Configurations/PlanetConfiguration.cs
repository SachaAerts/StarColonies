using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Data.Entities.Missions;

namespace StarColonies.Infrastructures.Data.Configurations;

public class PlanetConfiguration : IEntityTypeConfiguration<PlanetEntity>
{
    public void Configure(EntityTypeBuilder<PlanetEntity> builder)
    {
        builder.HasKey(m => m.Id);
        
        builder.Property(m => m.Name)
               .IsRequired()
               .HasMaxLength(100);
        
        builder.HasIndex(m => m.Name)
               .IsUnique();
        
        builder.Property(m => m.X)
               .IsRequired()
               .HasDefaultValue(100); 
        builder.Property(m => m.Y)
               .IsRequired()
               .HasDefaultValue(100);
        
        builder.Property(m => m.ImagePath)
               .IsRequired();

        builder.HasMany(m => m.Missions)
               .WithOne(p => p.Planet)
               .HasForeignKey(m => m.PlanetId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}