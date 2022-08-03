using MediatR;

namespace FurnitureStore.Application.CommandsQueries.Company.Commands.Create;

public class CreateCompanyCommand : IRequest<long>
{
    public string Name { get; set; }
}
