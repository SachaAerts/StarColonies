using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Data.Entities.Items;

namespace StarColonies.Infrastructures.Data.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<ItemEntity>
{
    public void Configure(EntityTypeBuilder<ItemEntity> builder)
    {
        builder.HasKey(m => m.Id);
        
        builder.Property(m => m.Name)
               .IsRequired()
               .HasMaxLength(100);
        
        builder.HasIndex(m => m.Name)
               .IsUnique();

        builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(300);
        
        builder.HasOne(i => i.Effect)
               .WithMany(e => e.Items)
               .HasForeignKey(i => i. EffectId);
        
        builder.Property(m => m.CoinsValue)
               .IsRequired()
               .HasDefaultValue(0);
        
        builder.Property(m => m.ImagePath)
               .IsRequired();
        
        builder.HasMany(i => i.Rewards)
               .WithOne(r => r.Item)
               .HasForeignKey(r => r.ItemId);
        
    }
}