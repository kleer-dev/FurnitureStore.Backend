using FurnitureStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurnitureStore.Persistence.EntityTypeConfigurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(company => company.Id);
        builder.HasIndex(company => company.Id).IsUnique();

        builder.Property(company => company.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasMany(company => company.Furnitures)
            .WithOne(furniture => furniture.Company);
    }
}
