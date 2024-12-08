using Moq;
using ProductFlow.Domain.Entities;
using ProductFlow.Domain.Repositories.Products;

namespace CommonTestUtilities.Repositories;

public class ProductsReadOnlyRepositoryBuilder
{
    private readonly Mock<IProductsReadOnlyRepository> _repository;
    
    public ProductsReadOnlyRepositoryBuilder()
    {
        _repository = new Mock<IProductsReadOnlyRepository>();
    }
    
    public ProductsReadOnlyRepositoryBuilder GetAll(List<Product> products)
    {
        _repository.Setup(repository => repository.GetAll()).ReturnsAsync(products);
        
        return this;
    }
    
    public ProductsReadOnlyRepositoryBuilder GetById(Product? product)
    {
        if (product is not null)
            _repository.Setup(repository => repository.GetById(product.Id)).ReturnsAsync(product);
        
        return this;
    }
    
    public ProductsReadOnlyRepositoryBuilder FilterProductInStock(List<Product> products)
    {
        _repository.Setup(repository => repository.FilterProductInStock()).ReturnsAsync(products);

        return this;
    }
    
    public IProductsReadOnlyRepository Build() => _repository.Object;
}