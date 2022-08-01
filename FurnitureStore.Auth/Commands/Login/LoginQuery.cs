using MediatR;

namespace FurnitureStore.Auth.Commands.Login;

public class LoginQuery : IRequest<AuthResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
