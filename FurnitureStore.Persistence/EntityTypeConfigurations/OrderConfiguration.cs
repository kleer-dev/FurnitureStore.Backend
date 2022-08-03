using FurnitureStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurnitureStore.Persistence.EntityTypeConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(order => order.Id);
        builder.HasIndex(order => order.Id).IsUnique();

        builder.Property(order => order.IsCompleted).HasDefaultValue(false);

        builder.HasOne(order => order.User)
            .WithMany(user => user.Orders);

        builder.HasOne(order => order.Furniture)
            .WithMany(furniture => furniture.Orders);
    }
}
