using System.Net;
using FurnitureStore.Auth.Interfaces;
using FurnitureStore.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FurnitureStore.Auth.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, UserDto>
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

    public async Task<UserDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            //throw new NotFoundException(nameof(Domain.User), request.Email);
            throw new Exception("User not found");
        }

        var result = await _signInManager
                .CheckPasswordSignInAsync(user, request.Password, false);

        if (result.Succeeded)
        {
            return new UserDto()
            {
                Name = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Token = _jwtGenerator.CreateToken(user),
                Balance = 0
            };
        }

        throw new Exception(HttpStatusCode.Unauthorized.ToString());
    }
}
