using ProductFlow.Domain.Repositories;
using ProductFlow.Domain.Repositories.User;
using ProductFlow.Domain.Services.LoggedUser;

namespace ProductFlow.Application.UseCases.User.Delete;

public class DeleteUserAccountUseCase : IDeleteUserAccountUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserAccountUseCase(
        ILoggedUser loggedUser, 
        IUserWriteOnlyRepository userWriteOnlyRepository,
        IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute()
    {
        var user = await _loggedUser.Get();
        
        await _userWriteOnlyRepository.Delete(user);
        
        await _unitOfWork.Commit();
    }
}