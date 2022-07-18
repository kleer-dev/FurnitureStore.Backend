namespace FurnitureStore.Domain;

public class Furniture
{
    public long Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public double Lenght { get; set; }
    public double Height { get; set; }
    public string Material { get; set; }

    public FurnitureType FurnitureType { get; set; }
    public Company Company { get; set; }
    public List<Order> Orders { get; set; }
}
