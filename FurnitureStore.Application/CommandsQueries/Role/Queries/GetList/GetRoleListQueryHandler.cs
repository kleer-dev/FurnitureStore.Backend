using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.Role.Queries.GetList;

public class GetRoleListQueryHandler : IRequestHandler<GetRoleListQuery, GetRoleListVm>
{
    private readonly RoleManager<IdentityRole<long>> _roleManager;
    private readonly IMapper _mapper;

    public GetRoleListQueryHandler(RoleManager<IdentityRole<long>> roleManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<GetRoleListVm> Handle(GetRoleListQuery request, 
        CancellationToken cancellationToken)
    {
        var roles = await _roleManager.Roles
            .ProjectTo<RoleDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetRoleListVm { Roles = roles };
    }
}