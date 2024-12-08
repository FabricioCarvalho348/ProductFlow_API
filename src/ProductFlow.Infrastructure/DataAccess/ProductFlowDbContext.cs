using Microsoft.EntityFrameworkCore;
using ProductFlow.Domain.Entities;
using ProductFlow.Infrastructure.DataAccess.Mappings;

namespace ProductFlow.Infrastructure.DataAccess;

public class ProductFlowDbContext : DbContext
{
    public ProductFlowDbContext(DbContextOptions options) : base(options) { }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductMap());

        modelBuilder.Entity<Category>().ToTable("Category");
    }
}