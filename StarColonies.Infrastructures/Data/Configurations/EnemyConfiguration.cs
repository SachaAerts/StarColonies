using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Entities.Missions;

namespace StarColonies.Infrastructures.Data.Configurations.Missions;

public class EnemyConfiguration : IEntityTypeConfiguration<EnemyEntity>
{
    public void Configure(EntityTypeBuilder<EnemyEntity> builder)
    {
        builder.HasKey(m => m.Id);
        
        builder.Property(m => m.Name)
               .IsRequired()
               .HasMaxLength(100);
        
        builder.Property(m => m.Strength)
               .IsRequired();
        
        builder.Property(m => m.Stamina)
               .IsRequired();
        
        builder.Property(m => m.ImagePath)
               .IsRequired()
               .HasMaxLength(200);
        
        builder.HasOne(e => e.Type)
               .WithMany(t => t.Enemies)
               .HasForeignKey(e => e.TypeId)
               .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(e => e.Missions)
               .WithMany(m => m.Enemies);
    }
}