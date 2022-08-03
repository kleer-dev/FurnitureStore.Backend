using FurnitureStore.Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.User.Commands.Update;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly UserManager<Domain.User> _userManager;

    public UpdateUserCommandHandler(UserManager<Domain.User> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<Unit> Handle(UpdateUserCommand request, 
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id.ToString());
        var isPhoneNumberExist = await _userManager.Users
            .AnyAsync(u => u.PhoneNumber == request.PhoneNumber && u.Id != request.Id, cancellationToken);

        if (user == null)
            throw new NotFoundException(nameof(Domain.User), request.Id!);

        if (isPhoneNumberExist)
            throw new Exception("Incorrect information was entered");
        
        if (!await _userManager.CheckPasswordAsync(user, request.OldPassword))
            throw new Exception("Password not correct");

        user.UserName = request.UserName;
        user.Email = request.Email;
        user.PhoneNumber = request.PhoneNumber;

        await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

        return Unit.Value;
    }
}