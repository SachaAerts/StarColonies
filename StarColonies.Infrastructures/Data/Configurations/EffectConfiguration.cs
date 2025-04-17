using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Domains.Models.Items;
using StarColonies.Infrastructures.Data.Entities.Items;

namespace StarColonies.Infrastructures.Data.Configurations.Items;

public class EffectConfiguration : IEntityTypeConfiguration<EffectEntity>
{
    public void Configure(EntityTypeBuilder<EffectEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(e => e.ForceModifier);
        builder.Property(e => e.StaminaModifier);
    }
}