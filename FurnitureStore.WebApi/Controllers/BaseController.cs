using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.WebApi.Controllers;

public abstract class BaseController : ControllerBase
{
    private IMediator? _mediator;

    protected IMediator Mediator =>
        _mediator ?? HttpContext.RequestServices.GetService<IMediator>()!;

    internal long? UserId => User.Identity!.IsAuthenticated
        ? Convert.ToInt64(User.FindFirstValue(ClaimTypes.NameIdentifier))
        : null;
}
