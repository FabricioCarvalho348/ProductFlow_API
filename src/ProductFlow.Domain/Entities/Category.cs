namespace ProductFlow.Domain.Entities;

public class Category
{
    public long Id { get; set; }
    public Enums.Category Value { get; set; }
    public long ProductId { get; set; }
    public Product Product { get; set; } = default!;
}