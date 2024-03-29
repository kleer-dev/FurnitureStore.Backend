﻿using FurnitureStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurnitureStore.Persistence.EntityTypeConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(user => user.Balance).IsRequired();
        
        builder.Property(u => u.RefreshToken).IsRequired(false);
        builder.Property(u => u.RefreshTokenExpiryTime).IsRequired(false);

        builder.HasMany(user => user.Orders)
            .WithOne(order => order.User);
    }
}
