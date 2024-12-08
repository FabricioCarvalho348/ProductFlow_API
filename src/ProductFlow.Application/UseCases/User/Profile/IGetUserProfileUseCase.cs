using ProductFlow.Communication.Responses.User;

namespace ProductFlow.Application.UseCases.User.Profile;

public interface IGetUserProfileUseCase
{
    Task<ResponseUserProfileJson> Execute();
}