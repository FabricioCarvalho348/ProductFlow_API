using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using ProductFlow.Application.UseCases.Products.GetAll;
using ProductFlow.Domain.Entities;

namespace UseCases.Test.Products.GetAll;

public class GetAllProductUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var products = ProductBuilder.Collection();

        var useCase = CreateUseCase(products);

        var result = await useCase.Execute();

        result.Should().NotBeNull();
        result.Products.Should().NotBeNullOrEmpty().And.AllSatisfy(product =>
        {
            product.Id.Should().BeGreaterThan(0);
            product.Name.Should().NotBeNullOrEmpty();
            product.Price.Should().BeGreaterThan(0);
        });
    }

    private GetAllProductUseCase CreateUseCase(List<Product> products)
    {
        var repository = new ProductsReadOnlyRepositoryBuilder().GetAll(products).Build();
        var mapper = MapperBuilder.Build();
        
        return new GetAllProductUseCase(repository, mapper);
    }
}