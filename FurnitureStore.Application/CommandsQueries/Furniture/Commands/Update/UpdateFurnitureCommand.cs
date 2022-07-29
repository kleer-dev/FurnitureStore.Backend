using MediatR;

namespace FurnitureStore.Application.CommandsQueries.Furniture.Commands.Update;

public class UpdateFurnitureCommand : IRequest
{
    public long Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public double Lenght { get; set; }
    public double Height { get; set; }
    public string Material { get; set; }

    public long furnitureTypeId { get; set; }
    public long companyId { get; set; }
}
