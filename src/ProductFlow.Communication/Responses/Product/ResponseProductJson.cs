using ProductFlow.Communication.Enums;

namespace ProductFlow.Communication.Responses.Product;

public class ResponseProductJson
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Status Status { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public IList<Category> Categories { get; set; } = [];
}