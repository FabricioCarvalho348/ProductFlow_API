using FluentValidation.Results;
using ProductFlow.Communication.Requests;
using ProductFlow.Domain.Repositories;
using ProductFlow.Domain.Repositories.User;
using ProductFlow.Domain.Security.Cryptography;
using ProductFlow.Domain.Services.LoggedUser;
using ProductFlow.Exception;
using ProductFlow.Exception.ExceptionBase;

namespace ProductFlow.Application.UseCases.User.ChangePassword;

public class ChangePasswordUseCase : IChangePasswordUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordEncrypter _passwordEncrypter;
    
    public ChangePasswordUseCase(
        ILoggedUser loggedUser,
        IUserUpdateOnlyRepository userUpdateOnlyRepository,
        IUnitOfWork unitOfWork,
        IPasswordEncrypter passwordEncrypter)
    {
        _loggedUser = loggedUser;
        _userUpdateOnlyRepository = userUpdateOnlyRepository;
        _unitOfWork = unitOfWork;
        _passwordEncrypter = passwordEncrypter;
    }
    
    public async Task Execute(RequestChangePasswordJson request)
    {
        var loggedUser = await _loggedUser.Get();

        Validate(request, loggedUser);

        var user = await _userUpdateOnlyRepository.GetById(loggedUser.Id);
        user.Password = _passwordEncrypter.Encrypt(request.NewPassword);
        
        _userUpdateOnlyRepository.Update(user);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestChangePasswordJson request, Domain.Entities.User loggedUser)
    {
        var validator = new ChangePasswordValidator();

        var result = validator.Validate(request);

        var passwordMatch = _passwordEncrypter.Verify(request.Password, loggedUser.Password);

        if (passwordMatch == false)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.PASSWORD_DIFFERENT_CURRENT_PASSWORD));
        }

        if (result.IsValid == false)
        {
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errors);
        }
    }
}