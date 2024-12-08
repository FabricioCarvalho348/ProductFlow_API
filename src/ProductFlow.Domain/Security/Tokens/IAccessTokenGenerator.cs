using ProductFlow.Domain.Entities;

namespace ProductFlow.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
    string Generate(User user);
}