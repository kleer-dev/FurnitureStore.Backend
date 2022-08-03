using AutoMapper;
using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.User.Queries.Get;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserVm>
{
    private readonly IFurnitureStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IFurnitureStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<UserVm> Handle(GetUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        var userVm = _mapper.Map<UserVm>(user);

        return userVm;
    }
}