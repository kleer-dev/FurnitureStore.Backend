using AutoMapper;
using FurnitureStore.Application.Interfaces;
using FurnitureStore.Auth.Exceptions;
using FurnitureStore.Auth.Interfaces;
using FurnitureStore.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Auth.Commands.Registration;

public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, AuthResponse>
{
    private readonly UserManager<Domain.User> _userManager;
    private readonly SignInManager<Domain.User> _signInManager;
    private readonly IFurnitureStoreDbContext _context;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IMapper _mapper;

    public RegistrationCommandHandler(UserManager<Domain.User> userManager,
        IJwtGenerator jwtGenerator, IFurnitureStoreDbContext context,
        SignInManager<Domain.User> signInManager, IMapper mapper)
    {
        _userManager = userManager;
        _jwtGenerator = jwtGenerator;
        _context = context;
        _signInManager = signInManager;
        _mapper = mapper;
    }

    public async Task<AuthResponse> Handle(RegistrationCommand request,
        CancellationToken cancellationToken)
    {
        if (await _context.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
            throw new UserRegistrationException("Email already exist");

        if (await _context.Users.AnyAsync(u => u.PhoneNumber == request.PhoneNumber, cancellationToken))
            throw new UserRegistrationException("Phone number already exist");

        var refreshToken = _jwtGenerator.GenerateRefreshToken();
        
        var user = new User()
        {
            UserName = request.Name,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7)
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            throw new UserRegistrationException(result.Errors.ToList());

        return new AuthResponse()
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Balance = user.Balance,
            Token = _jwtGenerator.CreateToken(user),
            RefreshToken = user.RefreshToken
        };;
    }
}
