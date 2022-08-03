using System.Net;
using FurnitureStore.Application.Common.Exceptions;
using FurnitureStore.Auth.Interfaces;
using FurnitureStore.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FurnitureStore.Auth.Commands.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;

    public LoginQueryHandler(UserManager<User> userManager,
        SignInManager<User> signInManager,
        IJwtGenerator jwtGenerator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<AuthResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            throw new NotFoundException(nameof(User), request.Email);

        var result = await _signInManager
                .CheckPasswordSignInAsync(user, request.Password, false);
        
        if (!result.Succeeded)
            throw new Exception(HttpStatusCode.Unauthorized.ToString());
        
        var refreshToken = _jwtGenerator.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            
        await _userManager.UpdateAsync(user);

        return new AuthResponse()
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Balance = user.Balance,
            Token = _jwtGenerator.CreateToken(user),
            RefreshToken = user.RefreshToken
        };
    }
}
