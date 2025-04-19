using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Infrastructures.Data.Configurations;

public class ColonieConfiguration : IEntityTypeConfiguration<ColonyEntity>
{
    public void Configure(EntityTypeBuilder<ColonyEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.Property(c => c.OwnerId)
            .IsRequired();
        
        builder.Property(c => c.LogoPath)
            .IsRequired();

        builder.HasOne(c => c.Owner)
            .WithMany()
            .HasForeignKey(c => c.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Members)
            .WithOne()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(c => c.MissionExecutions)
            .WithOne(e => e.Colony)
            .HasForeignKey(e => e.ColonieId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}