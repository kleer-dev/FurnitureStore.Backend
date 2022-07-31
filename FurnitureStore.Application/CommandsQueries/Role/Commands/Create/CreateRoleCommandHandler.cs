using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FurnitureStore.Application.CommandsQueries.Role.Commands.Create;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, long>
{
    private readonly RoleManager<IdentityRole<long>> _roleManager;

    public CreateRoleCommandHandler(RoleManager<IdentityRole<long>> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<long> Handle(CreateRoleCommand request, 
        CancellationToken cancellationToken)
    {
        var result = await _roleManager.CreateAsync(new IdentityRole<long>(request.Name));

        if (!result.Succeeded)
            throw new Exception("Error when creating a role");

        var roleId = _roleManager.FindByNameAsync(request.Name).Id;

        return roleId;
    }
}