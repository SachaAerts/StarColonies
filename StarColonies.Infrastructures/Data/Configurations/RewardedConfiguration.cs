using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Domains.Models.Items;
using StarColonies.Infrastructures.Data.Entities.Items;
using RewardedEntity = StarColonies.Infrastructures.Data.Entities.Items.RewardedEntity;

namespace StarColonies.Infrastructures.Data.Configurations.Items;

public class RewardedConfiguration : IEntityTypeConfiguration<RewardedEntity>
{
    public void Configure(EntityTypeBuilder<RewardedEntity> builder)
    {
        builder.HasKey(r => new { r.MissionId, r.ItemId });

        builder.HasOne(r => r.Mission)
            .WithMany(m => m.Rewards)
            .HasForeignKey(r => r.MissionId);

        builder.HasOne(r => r.Item)
            .WithMany(i => i.Rewards)
            .HasForeignKey(r => r.ItemId);

        builder.Property(r => r.Quantity)
            .IsRequired()
            .HasDefaultValue(1);
    }
}