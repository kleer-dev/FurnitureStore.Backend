using MediatR;

namespace FurnitureStore.Application.CommandsQueries.Role.Commands.Update;

public class UpdateRoleCommand : IRequest
{
    public long Id { get; set; }
    public string Name { get; set; }
}