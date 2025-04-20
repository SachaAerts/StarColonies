using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Infrastructures.Data.Configurations;

public class ColonyMemberConfiguration : IEntityTypeConfiguration<ColonyMemberEntity>
{
    public void Configure(EntityTypeBuilder<ColonyMemberEntity> builder)
    {
        builder.HasKey(cm => new { ColonieId = cm.ColonyId, cm.ColonistId });

        builder.HasOne(cm => cm.Colony)
            .WithMany(c => c.Members)
            .HasForeignKey(cm => cm.ColonyId);

        builder.HasOne(cm => cm.Colonist)
            .WithMany(c => c.Colonies)
            .HasForeignKey(cm => cm.ColonistId);
    }
}
