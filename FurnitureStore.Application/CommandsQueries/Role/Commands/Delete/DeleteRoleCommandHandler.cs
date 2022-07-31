using FurnitureStore.Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FurnitureStore.Application.CommandsQueries.Role.Commands.Delete;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Unit>
{
    private readonly RoleManager<IdentityRole<long>> _roleManager;

    public DeleteRoleCommandHandler(RoleManager<IdentityRole<long>> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Unit> Handle(DeleteRoleCommand request, 
        CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());

        if (role == null)
            throw new NotFoundException(nameof(IdentityRole<long>), request.RoleId);

        var result = await _roleManager.DeleteAsync(role);

        if (!result.Succeeded)
            throw new Exception("Error when deleting a role");

        return Unit.Value;
    }
}