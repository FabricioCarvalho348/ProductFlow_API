using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductFlow.Domain.Repositories;
using ProductFlow.Domain.Repositories.Products;
using ProductFlow.Domain.Repositories.User;
using ProductFlow.Domain.Security.Cryptography;
using ProductFlow.Domain.Security.Tokens;
using ProductFlow.Domain.Services.LoggedUser;
using ProductFlow.Infrastructure.DataAccess;
using ProductFlow.Infrastructure.DataAccess.Repositories;
using ProductFlow.Infrastructure.Extensions;
using ProductFlow.Infrastructure.Security.Tokens;
using ProductFlow.Infrastructure.Services.LoggedUser;

namespace ProductFlow.Infrastructure;

public static class InfrastructureModule
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPasswordEncrypter, Security.Cryptography.BCrypt>();
        services.AddScoped<ILoggedUser, LoggedUser>();
        
        AddToken(services, configuration);
        AddRepositories(services);

        if (configuration.IsTestEnvironment() == false)
        {
            AddDbContext(services, configuration);
        }
    }

    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");
        
        services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
    }
    

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductsWriteOnlyRepository, ProductRepository>();
        services.AddScoped<IProductsReadOnlyRepository, ProductRepository>();
        services.AddScoped<IProductsUpdateOnlyRepository, ProductRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();
        
        services.AddDbContext<ProductFlowDbContext>(dbContextOptions =>
        {
            dbContextOptions.UseSqlServer(connectionString);
        });
    }
}