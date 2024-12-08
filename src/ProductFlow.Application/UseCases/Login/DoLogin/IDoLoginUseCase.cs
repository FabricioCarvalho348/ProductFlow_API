using ProductFlow.Communication.Requests;
using ProductFlow.Communication.Responses.User;

namespace ProductFlow.Application.UseCases.Login.DoLogin;

public interface IDoLoginUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
}