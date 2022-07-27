using MediatR;

namespace FurnitureStore.Application.CommandsQueries.Company.Commands.Update;

public class UpdateCompanyCommand : IRequest
{
    public long Id { get; set; }
    public string Name { get; set; }
}
