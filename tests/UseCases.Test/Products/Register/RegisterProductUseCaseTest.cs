using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using ProductFlow.Application.UseCases.Products.Register;
using ProductFlow.Exception;
using ProductFlow.Exception.ExceptionBase;

namespace UseCases.Test.Products.Register;

public class RegisterProductUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestProductJsonBuilder.Build();
        var useCase = CreateUseCase();
        
        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);
    }
    
    [Fact]
    public async Task Error_Name_Empty()
    {
        var request = RequestProductJsonBuilder.Build();
        request.Name = string.Empty;
        
        var useCase = CreateUseCase();
        
        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.NAME_REQUIRED));
    }

    private RegisterProductUseCase CreateUseCase()
    {
        var repository = ProductsWriteOnlyRepositoryBuilder.Build(); 
        var unityOfWork = UnitOfWorkBuilder.Build();
        var mapper = MapperBuilder.Build();
        
        return new RegisterProductUseCase(repository, unityOfWork, mapper);
    }
}