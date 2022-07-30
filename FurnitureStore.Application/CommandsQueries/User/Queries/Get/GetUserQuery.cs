using MediatR;

namespace FurnitureStore.Application.CommandsQueries.User.Queries.Get;

public class GetUserQuery : IRequest<UserVm>
{
    public long? UserId { get; set; }
}