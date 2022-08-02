using FurnitureStore.Persistence.DbContexts;

namespace FurnitureStore.Persistence.Initializers;

public class LogDbInitializer
{
    public static void Initialize(LogDbContext dbContext) =>
        dbContext.Database.EnsureCreated();
}