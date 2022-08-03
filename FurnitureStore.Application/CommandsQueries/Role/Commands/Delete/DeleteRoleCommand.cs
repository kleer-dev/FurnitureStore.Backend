using MediatR;

namespace FurnitureStore.Application.CommandsQueries.Role.Commands.Delete;

public class DeleteRoleCommand : IRequest
{
    public long RoleId { get; set; }
}