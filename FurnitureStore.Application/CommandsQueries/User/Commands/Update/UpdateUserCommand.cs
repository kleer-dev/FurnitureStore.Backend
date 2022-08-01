using MediatR;

namespace FurnitureStore.Application.CommandsQueries.User.Commands.Update;

public class UpdateUserCommand : IRequest
{
    public long? Id { get; set; }
    public string PhoneNumber { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}