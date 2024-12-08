using CommonTestUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductFlow.Api;
using ProductFlow.Domain.Entities;
using ProductFlow.Domain.Enums;
using ProductFlow.Domain.Security.Cryptography;
using ProductFlow.Domain.Security.Tokens;
using ProductFlow.Infrastructure.DataAccess;
using WebApi.Test.Resources;

namespace WebApi.Test;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public ProductIdentityManager ProductManager { get; private set; } = default!;
    public ProductIdentityManager ProductEmployee { get; private set; } = default!;

    public UserIdentityManager UserMember { get; private set; } = default!;
    public UserIdentityManager UserManager { get; private set; } = default!;
    public UserIdentityManager UserEmployee { get; private set; } = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<ProductFlowDbContext>(config =>
                {
                    config.UseInMemoryDatabase("InMemoryDbForTesting");
                    config.UseInternalServiceProvider(provider);
                });

                var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<ProductFlowDbContext>();
                var passwordEncrypter = scope.ServiceProvider.GetRequiredService<IPasswordEncrypter>();
                var accessTokenGenerator = scope.ServiceProvider.GetRequiredService<IAccessTokenGenerator>();

                StartDatabase(dbContext, passwordEncrypter, accessTokenGenerator);
            });
    }

    private void StartDatabase(
        ProductFlowDbContext dbContext,
        IPasswordEncrypter passwordEncrypter,
        IAccessTokenGenerator accessTokenGenerator)
    {
        var userMember = AddUserMember(dbContext, passwordEncrypter, accessTokenGenerator);
        var userManager = AddUserManager(dbContext, passwordEncrypter, accessTokenGenerator);
        var userEmployee = AddUserEmployee(dbContext, passwordEncrypter, accessTokenGenerator);

        var productEmployee1 = AddProducts(dbContext, productId: 1, user: userEmployee, categoryId: 1);
        var productManager1 = AddProducts(dbContext, productId: 2, user: userManager, categoryId: 2);
        var productEmployee2 = AddProducts(dbContext, productId: 3, user: userEmployee, categoryId: 3);
        var productManager2 = AddProducts(dbContext, productId: 4, user: userManager, categoryId: 4);

        
        ProductManager = new ProductIdentityManager(productManager1);
        ProductManager = new ProductIdentityManager(productManager2);

        ProductEmployee = new ProductIdentityManager(productEmployee1);
        ProductEmployee = new ProductIdentityManager(productEmployee2);
        
        dbContext.Add(userMember);
        
        dbContext.SaveChanges();
    }

    private User AddUserMember(ProductFlowDbContext dbContext,
        IPasswordEncrypter passwordEncrypter,
        IAccessTokenGenerator accessTokenGenerator)
    {
        var user = UserBuilder.Build();
        user.Id = 1;

        var password = user.Password;
        user.Password = passwordEncrypter.Encrypt(user.Password);
        
        dbContext.Users.Add(user);

        var token = accessTokenGenerator.Generate(user);

        UserMember = new UserIdentityManager(user, password, token);
        
        return user;
    }
    
    private User AddUserManager(ProductFlowDbContext dbContext,
        IPasswordEncrypter passwordEncrypter,
        IAccessTokenGenerator accessTokenGenerator)
    {
        var user = UserBuilder.Build(Roles.Manager);
        user.Id = 2;

        var password = user.Password;
        user.Password = passwordEncrypter.Encrypt(user.Password);

        user.Role = Roles.Manager;
        
        dbContext.Users.Add(user);

        var token = accessTokenGenerator.Generate(user);

        UserManager = new UserIdentityManager(user, password, token);
        
        return user;
    }

    private User AddUserEmployee(ProductFlowDbContext dbContext,
        IPasswordEncrypter passwordEncrypter,
        IAccessTokenGenerator accessTokenGenerator)
    {
        var user = UserBuilder.Build(Roles.Employee);
        user.Id = 3;

        var password = user.Password;
        user.Password = passwordEncrypter.Encrypt(user.Password);

        user.Role = Roles.Employee;

        dbContext.Users.Add(user);

        var token = accessTokenGenerator.Generate(user);

        UserEmployee = new UserIdentityManager(user, password, token);
        
        return user;
    }

    private Product AddProducts(ProductFlowDbContext dbContext, User user, long productId, long categoryId)
    {
        var product = ProductBuilder.BuildForUsers(user);
        product.Id = productId;

        foreach (var category in product.Categories)
        {
            category.Id = categoryId;
            category.ProductId = productId;
        }

        dbContext.Products.Add(product);

        return product;
    }
}