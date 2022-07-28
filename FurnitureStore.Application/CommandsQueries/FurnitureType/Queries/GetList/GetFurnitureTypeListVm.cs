using FurnitureStore.Application.CommandsQueries.Company.Queries.GetList;

namespace FurnitureStore.Application.CommandsQueries.FurnitureType.Queries.GetList;

public class GetFurnitureTypeListVm
{
    public IEnumerable<FurnitureTypeDto> FurnitureTypes { get; set; }
}
