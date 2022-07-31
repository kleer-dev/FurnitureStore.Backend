using MediatR;

namespace FurnitureStore.Application.CommandsQueries.Role.Commands.Create;

public class CreateRoleCommand : IRequest<long>
{
    public string Name { get; set; }
}