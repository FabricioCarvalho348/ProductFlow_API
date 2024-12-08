using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ProductFlow.Domain.Entities;
using ProductFlow.Domain.Enums;
using ProductFlow.Domain.Repositories.Products;
using Category = ProductFlow.Domain.Entities.Category;

namespace ProductFlow.Infrastructure.DataAccess.Repositories;

public class ProductRepository : IProductsWriteOnlyRepository, IProductsUpdateOnlyRepository, IProductsReadOnlyRepository
{
    
    private readonly ProductFlowDbContext _dbContext;
    
    public ProductRepository(ProductFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(Product product)
    {
        await _dbContext.Products.AddAsync(product);
    }

    public async Task<List<Product>> GetAll()
    {
        return await _dbContext.Products.AsNoTracking().ToListAsync();
    }

    async Task<Product?> IProductsReadOnlyRepository.GetById(long id)
    {
        return await GetFullExpense()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);     
    }

    async Task<Product?> IProductsUpdateOnlyRepository.GetById(long id)
    {
        return await GetFullExpense()
            .FirstOrDefaultAsync(e => e.Id == id);   
    }
    public async Task<List<Product>> FilterProductInStock()
    {
        return await _dbContext.Products.AsNoTracking().Where(p => p.Status == Status.InStock).ToListAsync();
    }

    public void Update(Product product)
    {
        _dbContext.Products.Update(product);
    }
    
    public async Task Delete(long id)
    {
        var result = await _dbContext.Products.FindAsync(id);
        
        _dbContext.Products.Remove(result!);
    }
    
    private IIncludableQueryable<Product, ICollection<Category>> GetFullExpense()
    {
        return _dbContext.Products.Include(expense => expense.Categories);
    }
}