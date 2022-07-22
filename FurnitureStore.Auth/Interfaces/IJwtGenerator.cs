using FurnitureStore.Domain;

namespace FurnitureStore.Auth.Interfaces;

public interface IJwtGenerator
{
    string CreateToken(User user);
}