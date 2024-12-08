using ProductFlow.Domain.Repositories;

namespace ProductFlow.Infrastructure.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly ProductFlowDbContext _dbContext;
    
    public UnitOfWork(ProductFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task Commit() => await _dbContext.SaveChangesAsync();
}