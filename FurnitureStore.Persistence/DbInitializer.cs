namespace FurnitureStore.Persistence;

public class DbInitializer
{
    public static void Initialize(FurnitureStoreDbContext dbContext) =>
        dbContext.Database.EnsureCreated();
}
