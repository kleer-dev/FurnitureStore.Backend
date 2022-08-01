using MediatR;

namespace FurnitureStore.Auth.RefreshToken;

public class RefreshTokenCommand : IRequest<AuthResponse>
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}