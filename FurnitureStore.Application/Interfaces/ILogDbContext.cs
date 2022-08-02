using FurnitureStore.Domain;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.Interfaces;

public interface ILogDbContext
{
    DbSet<Log> Logs { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}