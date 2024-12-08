using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using ProductFlow.Application.UseCases.Products.Update;
using ProductFlow.Domain.Entities;
using ProductFlow.Domain.Enums;
using ProductFlow.Exception;
using ProductFlow.Exception.ExceptionBase;

namespace UseCases.Test.Products.Update;

public class UpdateProductUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestProductJsonBuilder.Build();
        var product = ProductBuilder.Build();

        var useCase = CreateUseCase(product);
        
        var act = async () => await useCase.Execute(product.Id, request);

        await act.Should().NotThrowAsync();
        
        product.Name.Should().Be(request.Name);
        product.Status.Should().Be((Status)request.Status);
        product.Price.Should().Be(request.Price);
        product.StockQuantity.Should().Be(request.StockQuantity);
    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        var product = ProductBuilder.Build();

        var request = RequestProductJsonBuilder.Build();
        request.Name = string.Empty;

        var useCase = CreateUseCase(product);

        var act = async () => await useCase.Execute(product.Id, request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.NAME_REQUIRED));
    }

    [Fact]
    public async Task Error_Product_Not_Found()
    {
        var request = RequestProductJsonBuilder.Build();

        var useCase = CreateUseCase();

        var act = async () => await useCase.Execute(id: 1000, request);

        var result = await act.Should().ThrowAsync<NotFoundException>();

        result.Where(
            ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.PRODUCT_NOT_FOUND));
    }
    
    
    private UpdateProductUseCase CreateUseCase(Product? product = null)
    {
        var repository = new ProductsUpdateOnlyRepositoryBuilder().GetById(product).Build();
        var unityOfWork = UnitOfWorkBuilder.Build();
        var mapper = MapperBuilder.Build();
        
        return new UpdateProductUseCase(mapper, unityOfWork, repository);
    }
}