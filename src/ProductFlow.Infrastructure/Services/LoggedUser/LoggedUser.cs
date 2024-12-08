using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using ProductFlow.Domain.Entities;
using ProductFlow.Domain.Security.Tokens;
using ProductFlow.Domain.Services.LoggedUser;
using ProductFlow.Infrastructure.DataAccess;

namespace ProductFlow.Infrastructure.Services.LoggedUser;

public class LoggedUser : ILoggedUser
{
    private readonly ProductFlowDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(ProductFlowDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> Get()
    {
        string token = _tokenProvider.TokenOnRequest();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

        return await _dbContext
            .Users
            .AsNoTracking()
            .FirstAsync(user => user.UserIdentifier == Guid.Parse(identifier));
    }
}