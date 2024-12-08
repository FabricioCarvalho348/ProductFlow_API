namespace ProductFlow.Domain.Security.Tokens;

public interface ITokenProvider
{
    string TokenOnRequest();
}