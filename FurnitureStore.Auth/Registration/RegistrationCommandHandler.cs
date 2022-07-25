using FurnitureStore.Application.Interfaces;
using FurnitureStore.Auth.Exceptions;
using FurnitureStore.Auth.Interfaces;
using FurnitureStore.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Auth.Registration;

public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, UserDto>
{
    private readonly UserManager<Domain.User> _userManager;
    private readonly SignInManager<Domain.User> _signInManager;
    private readonly IFurnitureStoreDbContext _context;
    private readonly IJwtGenerator _jwtGenerator;

    public RegistrationCommandHandler(UserManager<Domain.User> userManager,
        IJwtGenerator jwtGenerator, IFurnitureStoreDbContext context,
        SignInManager<Domain.User> signInManager)
    {
        _userManager = userManager;
        _jwtGenerator = jwtGenerator;
        _context = context;
        _signInManager = signInManager;
    }

    public async Task<UserDto> Handle(RegistrationCommand request,
        CancellationToken cancellationToken)
    {
        if (await _context.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
        {
            throw new UserRegistrationException("Email already exist");
        }

        if (await _context.Users.AnyAsync(u => u.PhoneNumber == request.PhoneNumber, cancellationToken))
        {
            throw new UserRegistrationException("Phone number already exist");
        }

        var user = new User()
        {
            UserName = request.Name,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            throw new UserRegistrationException(result.Errors.ToList());
        }

        return new UserDto()
        {
            Name = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Balance = user.Balance,
            Token = _jwtGenerator.CreateToken(user)
        };
    }
}
