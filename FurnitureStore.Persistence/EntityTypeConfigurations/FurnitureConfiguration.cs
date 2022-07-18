using FurnitureStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurnitureStore.Persistence.EntityTypeConfigurations;

public class FurnitureConfiguration : IEntityTypeConfiguration<Furniture>
{
    public void Configure(EntityTypeBuilder<Furniture> builder)
    {
        builder.HasKey(furniture => furniture.Id);
        builder.HasIndex(furniture => furniture.Id).IsUnique();

        builder.Property(furniture => furniture.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(furniture => furniture.Price).IsRequired();
        builder.Property(furniture => furniture.Lenght).IsRequired();
        builder.Property(furniture => furniture.Height).IsRequired();

        builder.Property(furniture => furniture.Material)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasOne(furniture => furniture.FurnitureType)
            .WithMany(fType => fType.Furnitures);

        builder.HasOne(furniture => furniture.Company)
            .WithMany(company => company.Furnitures);

        builder.HasMany(furniture => furniture.Orders)
            .WithOne(order => order.Furniture);
    }
}
