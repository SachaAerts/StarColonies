using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Domains.Models;

namespace StarColonies.Infrastructures.Data.Configurations;

public class MissionConfiguration : IEntityTypeConfiguration<MissionModel>
{
    public void Configure(EntityTypeBuilder<MissionModel> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Name).IsRequired().HasMaxLength(100);

        builder.HasOne(m => m.Planet)
            .WithMany(p => p.Missions)
            .HasForeignKey(m => m.PlanetId);
    }
}