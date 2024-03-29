﻿using MediatR;

namespace FurnitureStore.Application.CommandsQueries.Role.Commands.SetRole;

public class SetRoleCommand : IRequest
{
    public long UserId { get; set; }
    public long RoleId { get; set; }
}