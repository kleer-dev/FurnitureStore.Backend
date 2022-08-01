﻿using MediatR;

namespace FurnitureStore.Auth.Login;

public class LoginQuery : IRequest<AuthResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
