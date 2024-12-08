using ProductFlow.Domain.Entities;

namespace ProductFlow.Domain.Repositories.Products;

public interface IProductsReadOnlyRepository
{
    Task<List<Product>> GetAll();
    
    Task<Product?> GetById(long id);
    
    Task<List<Product>> FilterProductInStock();
}