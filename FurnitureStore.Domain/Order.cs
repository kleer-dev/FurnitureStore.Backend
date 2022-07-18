namespace FurnitureStore.Domain;

public class Order
{
    public long Id { get; set; }
    public int Count { get; set; }
    public decimal OrderPrice { get; set; }

    public User User { get; set; }  
    public Furniture Furniture { get; set; }
}
