using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Domains.Models;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Entities.Missions;

namespace StarColonies.Infrastructures.Data.Configurations;

public class MissionConfiguration : IEntityTypeConfiguration<MissionEntity>
{
    public void Configure(EntityTypeBuilder<MissionEntity> builder)
    {
        builder.HasKey(m => m.Id);
        
        builder.Property(m => m.Difficulty)
               .IsRequired();

        builder.Property(m => m.Name)
               .IsRequired()
               .HasMaxLength(100);
        
        builder.Property(m => m.Description)
               .IsRequired()
               .HasMaxLength(500);
        
        builder.Property(m => m.CoinsReward)
               .IsRequired()
               .HasDefaultValue(0);
        
        builder.HasOne(m => m.Planet)
               .WithMany(p => p.Missions)
               .HasForeignKey(m => m.PlanetId)
               .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(m => m.Rewards)
               .WithOne(r => r.Mission)
               .HasForeignKey(r => r.MissionId);
        
        builder.HasMany(m => m.Enemies)
               .WithMany(e => e.Missions);
    }
}