using System.Security.Claims;
using FurnitureStore.Domain;

namespace FurnitureStore.Auth.Interfaces;

public interface IJwtGenerator
{
    string CreateToken(User user);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}