using MediatR;

namespace FurnitureStore.Application.CommandsQueries.Furniture.Queries.Get;

public class GetFurnitureQuery : IRequest<FurnitureVm>
{
    public long Id { get; set; }
}
