using ProductFlow.Domain.Enums;

namespace ProductFlow.Domain.Entities;

public class Product
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Status Status { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public ICollection<Category> Categories { get; set; } = [];
}