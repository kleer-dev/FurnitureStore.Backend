using MediatR;

namespace FurnitureStore.Application.CommandsQueries.FurnitureType.Queries.Get;

public class GetFurnitureTypeQuery : IRequest<FurnitureTypeVm>
{
    public long Id { get; set; }
}
