using ProductFlow.Domain.Entities;

namespace ProductFlow.Domain.Repositories.Products;

public interface IProductsUpdateOnlyRepository
{
    Task<Product?> GetById(long id);
    void Update(Product product);
}