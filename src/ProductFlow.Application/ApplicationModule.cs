using Microsoft.Extensions.DependencyInjection;
using ProductFlow.Application.AutoMapper;
using ProductFlow.Application.UseCases.Login.DoLogin;
using ProductFlow.Application.UseCases.Products.Delete;
using ProductFlow.Application.UseCases.Products.GetAll;
using ProductFlow.Application.UseCases.Products.GetById;
using ProductFlow.Application.UseCases.Products.GetByInStock;
using ProductFlow.Application.UseCases.Products.Register;
using ProductFlow.Application.UseCases.Products.Update;
using ProductFlow.Application.UseCases.User.ChangePassword;
using ProductFlow.Application.UseCases.User.Delete;
using ProductFlow.Application.UseCases.User.Profile;
using ProductFlow.Application.UseCases.User.Register;
using ProductFlow.Application.UseCases.User.Update;

namespace ProductFlow.Application;

public static class ApplicationModule
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterProductUseCase, RegisterProductUseCase>();
        services.AddScoped<IGetAllProductUseCase, GetAllProductUseCase>();
        services.AddScoped<IGetProductByIdUseCase, GetProductByIdUseCase>();
        services.AddScoped<IGetProductByInStockUseCase, GetProductByInStockUseCase>();
        services.AddScoped<IDeleteProductUseCase, DeleteProductUseCase>();
        services.AddScoped<IUpdateProductUseCase, UpdateProductUseCase>();
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
        services.AddScoped<IDeleteUserAccountUseCase, DeleteUserAccountUseCase>();
    }
}