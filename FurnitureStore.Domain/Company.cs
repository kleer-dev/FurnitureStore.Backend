namespace FurnitureStore.Domain;

public class Company
{
    public long Id { get; set; }
    public string Name { get; set; }

    public List<Furniture> Furnitures { get; set; }
}
