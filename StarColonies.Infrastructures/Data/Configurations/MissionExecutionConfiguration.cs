using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Data.Entities.Missions;

namespace StarColonies.Infrastructures.Data.Configurations;

public class MissionExecutionConfiguration :IEntityTypeConfiguration<MissionExecutionEntity>
{
    public void Configure(EntityTypeBuilder<MissionExecutionEntity> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.ExecutedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(m => m.IsSuccess)
               .IsRequired();

        builder.Property(m => m.RewardedCoins)
               .IsRequired();

        builder.HasOne(e => e.Colonie)
               .WithMany(c => c.MissionExecutions)
               .HasForeignKey(e => e.ColonieId)
               .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(e => e.Mission)
               .WithMany()
               .HasForeignKey(e => e.MissionId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}