using AutoMapper;
using FurnitureStore.Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FurnitureStore.Application.CommandsQueries.Role.Queries.Get;

public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, RoleVm>
{
    private readonly RoleManager<IdentityRole<long>> _roleManager;
    private readonly IMapper _mapper;

    public GetRoleQueryHandler(RoleManager<IdentityRole<long>> roleManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<RoleVm> Handle(GetRoleQuery request, 
        CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        
        if (role == null)
            throw new NotFoundException(nameof(IdentityRole<long>), request.RoleId);

        return _mapper.Map<RoleVm>(role);
    }
}