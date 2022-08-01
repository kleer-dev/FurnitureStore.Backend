using MediatR;

namespace FurnitureStore.Auth.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<AuthResponse>
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}