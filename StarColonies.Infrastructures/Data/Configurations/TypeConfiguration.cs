using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Infrastructures.Data.Configurations;

public class TypeConfiguration : IEntityTypeConfiguration<TypeEntity>
{
    public void Configure(EntityTypeBuilder<TypeEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(t => t.Enemies)
            .WithOne(e => e.Type)
            .HasForeignKey(e => e.TypeId);
    }
}