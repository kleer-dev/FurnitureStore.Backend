using MediatR;

namespace FurnitureStore.Application.CommandsQueries.Role.Queries.Get;

public class GetRoleQuery : IRequest<RoleVm>
{
    public long RoleId { get; set; }
}