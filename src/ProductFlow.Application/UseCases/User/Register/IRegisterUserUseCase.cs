using ProductFlow.Communication.Requests;
using ProductFlow.Communication.Responses.User;

namespace ProductFlow.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
}