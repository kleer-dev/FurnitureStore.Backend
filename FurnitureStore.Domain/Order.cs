namespace FurnitureStore.Domain;

public class Order
{
    public long Id { get; set; }
    public User User { get; set; }
    public Furniture Furniture { get; set; }
}
