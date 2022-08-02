using FurnitureStore.Application.Interfaces;
using FurnitureStore.Domain;
using FurnitureStore.Persistence.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Persistence.DbContexts;

public class LogDbContext : DbContext, ILogDbContext
{
    public DbSet<Logs> Logs { get; set; }

    public LogDbContext(DbContextOptions<LogDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new LogsConfiguration());
        builder.Entity<Logs>().ToTable("logs");
        
        base.OnModelCreating(builder);
    }
}