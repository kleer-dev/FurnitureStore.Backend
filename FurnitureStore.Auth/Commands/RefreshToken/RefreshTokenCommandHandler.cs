using System.Security.Claims;
using FurnitureStore.Application.Common.Exceptions;
using FurnitureStore.Auth.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FurnitureStore.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly UserManager<Domain.User> _userManager;

    public RefreshTokenCommandHandler(IJwtGenerator jwtGenerator, 
        UserManager<Domain.User> userManager)
    {
        _jwtGenerator = jwtGenerator;
        _userManager = userManager;
    }
    
    public async Task<AuthResponse> Handle(RefreshTokenCommand request, 
        CancellationToken cancellationToken)
    {
        
        var principal = _jwtGenerator.GetPrincipalFromExpiredToken(request.Token);
        var userId = Convert.ToInt64(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (userId == null || user == null)
            throw new NotFoundException(nameof(Domain.User), null!);
        
        if (user.RefreshToken != request.RefreshToken ||
            user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            throw new Exception("Refresh token error");

        var newToken = _jwtGenerator.CreateToken(user);
        var newRefreshToken = _jwtGenerator.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _userManager.UpdateAsync(user);

        return new AuthResponse()
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Balance = user.Balance,
            Token = newToken,
            RefreshToken = newRefreshToken
        };
    }
}