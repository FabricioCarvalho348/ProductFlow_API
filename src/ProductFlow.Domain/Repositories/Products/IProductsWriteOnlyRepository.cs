using ProductFlow.Domain.Entities;

namespace ProductFlow.Domain.Repositories.Products;

public interface IProductsWriteOnlyRepository
{
    Task Add(Product product);
    
    Task Delete(long id);
}