using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Domains.Models;

namespace StarColonies.Infrastructures.Data.Configurations;

public class MissionConfiguration : IEntityTypeConfiguration<MissionModel>
{
    public void Configure(EntityTypeBuilder<MissionModel> builder)
    {
        builder.HasKey(m => m.Id);
        
        builder.Property(m => m.Difficulty)
               .IsRequired();

        builder.Property(m => m.Name)
               .IsRequired()
               .HasMaxLength(100);
        
        builder.Property(m => m.CoinsReward)
               .IsRequired()
               .HasDefaultValue(0);
        
        builder.HasOne(m => m.Planet)
               .WithMany(p => p.Missions)
               .HasForeignKey(m => m.PlanetId)
               .OnDelete(DeleteBehavior.Cascade); //Cascade delete if the planet is deleted
        
        builder.HasMany(m => m.ItemsToWin)
               .WithMany(i => i.Missions);
        
        builder.HasMany(m => m.Enemies)
               .WithMany(e => e.Missions);
    }
}