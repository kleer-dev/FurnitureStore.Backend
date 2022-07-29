using MediatR;

namespace FurnitureStore.Application.CommandsQueries.Furniture.Commands.Create;

public class CreateFurnitureCommand : IRequest<long>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public double Lenght { get; set; }
    public double Height { get; set; }
    public string Material { get; set; }

    public long FurnitureTypeId { get; set; }
    public long CompanyId { get; set; }
}
