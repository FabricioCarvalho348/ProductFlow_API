using AutoMapper;
using FluentValidation.Results;
using ProductFlow.Communication.Requests;
using ProductFlow.Communication.Responses.User;
using ProductFlow.Domain.Repositories;
using ProductFlow.Domain.Repositories.User;
using ProductFlow.Domain.Security.Cryptography;
using ProductFlow.Domain.Security.Tokens;
using ProductFlow.Exception;
using ProductFlow.Exception.ExceptionBase;

namespace ProductFlow.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IMapper _mapper;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IAccessTokenGenerator _tokenGenerator;
    private readonly IUnitOfWork _unitOfWork;
    
    public RegisterUserUseCase(IMapper mapper, IPasswordEncrypter passwordEncrypter, IUserReadOnlyRepository userReadOnlyRepository, IUserWriteOnlyRepository userWriteOnlyRepository, IUnitOfWork unitOfWork, IAccessTokenGenerator tokenGenerator)
    {
        _mapper = mapper;
        _passwordEncrypter = passwordEncrypter;
        _userReadOnlyRepository = userReadOnlyRepository;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _tokenGenerator = tokenGenerator;
    }
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);
        
        var user = _mapper.Map<Domain.Entities.User>(request);
        user.Password = _passwordEncrypter.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();

        await _userWriteOnlyRepository.Add(user);
        await _unitOfWork.Commit();
        
        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Token = _tokenGenerator.Generate(user)
        };
    }
    private async Task Validate(RequestRegisterUserJson request)
    {
        var result = await new RegisterUserValidator().ValidateAsync(request);
        
        var emailExists = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);

        if (emailExists)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
        }
        
        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}