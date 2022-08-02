using FurnitureStore.Domain;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.Interfaces;

public interface ILogDbContext
{
    DbSet<Logs> Logs { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}