using FurnitureStore.Persistence.DbContexts;

namespace FurnitureStore.Persistence.Initializers;

public class DbInitializer
{
    public static void Initialize(FurnitureStoreDbContext dbContext) =>
        dbContext.Database.EnsureCreated();
}
