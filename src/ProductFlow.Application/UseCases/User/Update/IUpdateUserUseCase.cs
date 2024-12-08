using ProductFlow.Communication.Requests;

namespace ProductFlow.Application.UseCases.User.Update;

public interface IUpdateUserUseCase
{
    Task Execute(RequestUpdateUserJson request);
}