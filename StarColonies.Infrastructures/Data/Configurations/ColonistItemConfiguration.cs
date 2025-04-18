using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Data.Entities.Items;

namespace StarColonies.Infrastructures.Data.Configurations;

public class ColonistItemConfiguration : IEntityTypeConfiguration<ColonistItemEntity>
{
    public void Configure(EntityTypeBuilder<ColonistItemEntity> builder)
    {
        builder.HasKey(ci => new { ci.ColonistId, ci.ItemId });

        builder
            .HasOne(ci => ci.Colonist)
            .WithMany(c => c.Inventory)
            .HasForeignKey(ci => ci.ColonistId);

        builder
            .HasOne(ci => ci.Item)
            .WithMany(i => i.Colonists)
            .HasForeignKey(ci => ci.ItemId);
    }
}