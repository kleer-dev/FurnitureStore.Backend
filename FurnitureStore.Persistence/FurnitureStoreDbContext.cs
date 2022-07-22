using FurnitureStore.Application.Interfaces;
using FurnitureStore.Domain;
using FurnitureStore.Persistence.EntityTypeConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Persistence;

public class FurnitureStoreDbContext : IdentityDbContext<User, IdentityRole<long>, long>,
    IFurnitureStoreDbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<FurnitureType> FurnitureTypes { get; set; }
    public DbSet<Furniture> Furnitures { get; set; }
    public DbSet<Company> Companies { get; set; }

    public FurnitureStoreDbContext(DbContextOptions<FurnitureStoreDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CompanyConfiguration());
        builder.ApplyConfiguration(new FurnitureConfiguration());
        builder.ApplyConfiguration(new FurnitureTypeConfiguration());
        builder.ApplyConfiguration(new OrderConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());

        base.OnModelCreating(builder);
    }
}
