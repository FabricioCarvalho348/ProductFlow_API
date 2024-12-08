using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using ProductFlow.Application.UseCases.Products.GetById;
using ProductFlow.Communication.Enums;
using ProductFlow.Domain.Entities;
using ProductFlow.Exception;
using ProductFlow.Exception.ExceptionBase;

namespace UseCases.Test.Products.GetById;

public class GetProductByIdUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var product = ProductBuilder.Build();
        
        var useCase = CreateUseCase(product);

        var result = await useCase.Execute(id: product.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(product.Id);
        result.Name.Should().Be(product.Name);
        result.Price.Should().Be(product.Price);
        result.Status.Should().Be((Status)(product.Status));
        result.Categories.Should().NotBeNullOrEmpty().And.BeEquivalentTo(product.Categories.Select(category => category.Value));
    }

    [Fact]
    public async Task Error_Product_Not_Found()
    {
        var useCase = CreateUseCase(null!);

        var act = async () => await useCase.Execute(id: 1000);

        var result = await act.Should().ThrowAsync<NotFoundException>();

        result.Where(
            ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.PRODUCT_NOT_FOUND));
    }
    
    private GetProductByIdUseCase CreateUseCase(Product product)
    {
        var repository = new ProductsReadOnlyRepositoryBuilder().GetById(product).Build();
        var mapper = MapperBuilder.Build();
        
        return new GetProductByIdUseCase(repository, mapper);
    }
}