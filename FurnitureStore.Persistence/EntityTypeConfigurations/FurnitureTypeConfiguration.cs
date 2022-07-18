using FurnitureStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurnitureStore.Persistence.EntityTypeConfigurations;

public class FurnitureTypeConfiguration : IEntityTypeConfiguration<FurnitureType>
{
    public void Configure(EntityTypeBuilder<FurnitureType> builder)
    {
        builder.HasKey(fType => fType.Id);
        builder.HasIndex(fType => fType.Id).IsUnique();

        builder.Property(fType => fType.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasMany(fType => fType.Furnitures)
            .WithOne(fType => fType.FurnitureType);
    }
}
