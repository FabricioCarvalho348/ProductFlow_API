using AutoMapper;
using ProductFlow.Communication.Responses.User;
using ProductFlow.Domain.Services.LoggedUser;

namespace ProductFlow.Application.UseCases.User.Profile;

public class GetUserProfileUseCase : IGetUserProfileUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;
    
    public GetUserProfileUseCase(ILoggedUser loggedUser, IMapper mapper)
    {
        _loggedUser = loggedUser;
        _mapper = mapper;
    }
    
    public async Task<ResponseUserProfileJson> Execute()
    {
        var user = await _loggedUser.Get();
        
        return _mapper.Map<ResponseUserProfileJson>(user);
    }   
}