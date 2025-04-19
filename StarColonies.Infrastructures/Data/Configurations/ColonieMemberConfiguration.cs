using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Infrastructures.Data.Configurations;

public class ColonieMemberConfiguration : IEntityTypeConfiguration<ColonieMemberEntity>
{
    public void Configure(EntityTypeBuilder<ColonieMemberEntity> builder)
    {
        builder.HasKey(cm => new { cm.ColonieId, cm.ColonistId });

        builder.HasOne(cm => cm.Colony)
            .WithMany(c => c.Members)
            .HasForeignKey(cm => cm.ColonieId);

        builder.HasOne(cm => cm.Colonist)
            .WithMany(c => c.Colonies)
            .HasForeignKey(cm => cm.ColonistId);
    }
}
