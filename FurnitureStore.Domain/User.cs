namespace FurnitureStore.Domain;

public class User
{
    public decimal Balance { get; set; }

    public List<Order> Orders { get; set; }
}
