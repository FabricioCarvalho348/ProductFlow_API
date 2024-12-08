using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductFlow.Domain.Entities;

namespace ProductFlow.Infrastructure.DataAccess.Mappings;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasMaxLength(80);
        
        builder.Property(p => p.Status)
            .IsRequired()
            .HasColumnName("Status")
            .HasMaxLength(50)
            .HasConversion<string>();
        
        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("DECIMAL(18,2)")
            .HasColumnName("Amount");

        builder.Property(p => p.StockQuantity)
            .IsRequired()
            .HasColumnName("StockQuantity");
        
        builder.HasMany(p => p.Categories)
            .WithOne(c => c.Product)
            .HasForeignKey(c => c.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}