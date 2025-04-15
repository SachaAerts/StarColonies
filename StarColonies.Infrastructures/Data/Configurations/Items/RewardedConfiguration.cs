using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Domains.Models.Items;

namespace StarColonies.Infrastructures.Data.Configurations.Items;

public class RewardedConfiguration : IEntityTypeConfiguration<RewardedModel>
{
    public void Configure(EntityTypeBuilder<RewardedModel> builder)
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