using FurnitureStore.Domain;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.Interfaces;

public interface IFurnitureStoreDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<FurnitureType> FurnitureTypes { get; set; }
    DbSet<Furniture> Furnitures { get; set; }
    DbSet<Company> Companies { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
