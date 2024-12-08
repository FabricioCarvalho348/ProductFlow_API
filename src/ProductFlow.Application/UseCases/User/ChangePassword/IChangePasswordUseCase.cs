using ProductFlow.Communication.Requests;

namespace ProductFlow.Application.UseCases.User.ChangePassword;

public interface IChangePasswordUseCase
{
    Task Execute(RequestChangePasswordJson request);
}