using Moq;
using ProductFlow.Domain.Entities;
using ProductFlow.Domain.Repositories.Products;

namespace CommonTestUtilities.Repositories;

public class ProductsUpdateOnlyRepositoryBuilder
{
    private readonly Mock<IProductsUpdateOnlyRepository> _repository;
    
    public ProductsUpdateOnlyRepositoryBuilder()
    {
        _repository = new Mock<IProductsUpdateOnlyRepository>();
    }
    
    public ProductsUpdateOnlyRepositoryBuilder GetById(Product? product)
    {
        if (product is not null)
            _repository.Setup(repository => repository.GetById(product.Id)).ReturnsAsync(product);
        
        return this;
    }

    public IProductsUpdateOnlyRepository Build() => _repository.Object;
}