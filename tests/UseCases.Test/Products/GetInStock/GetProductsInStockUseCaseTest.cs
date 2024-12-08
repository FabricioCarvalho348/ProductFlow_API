using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using ProductFlow.Application.UseCases.Products.GetByInStock;
using ProductFlow.Domain.Entities;
using ProductFlow.Domain.Enums;

namespace UseCases.Test.Products.GetInStock;

public class GetProductsInStockUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var products = ProductBuilder.CollectionInStock();
        
        var inStockProducts = products.Where(p => p.Status == Status.InStock).ToList();

        var useCase = CreateUseCase(inStockProducts);
        
        var result = await useCase.Execute();

        result.Should().NotBeNull();
        result.Products.Should().NotBeNullOrEmpty().And.AllSatisfy(product =>
        {
            product.Id.Should().BeGreaterThan(0);
            product.Name.Should().NotBeNullOrEmpty();
            product.Price.Should().BeGreaterThan(0);
            product.Status.Should().Be(ProductFlow.Communication.Enums.Status.InStock);
        });
    }

    private GetProductByInStockUseCase CreateUseCase(List<Product> products)
    {
        var repository = new ProductsReadOnlyRepositoryBuilder().FilterProductInStock(products).Build();
        var mapper = MapperBuilder.Build();
        
        return new GetProductByInStockUseCase(repository, mapper);
    }
}