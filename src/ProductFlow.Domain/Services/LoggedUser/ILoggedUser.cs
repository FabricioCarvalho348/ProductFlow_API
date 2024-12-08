using ProductFlow.Domain.Entities;

namespace ProductFlow.Domain.Services.LoggedUser;

public interface ILoggedUser
{
    Task<User> Get();
}