using ProductFlow.Domain.Entities;

namespace WebApi.Test.Resources;

public class ProductIdentityManager
{
    private readonly Product _product;

    public ProductIdentityManager(Product product)
    {
        _product = product;
    }

    public long GetId() => _product.Id;
    
    public string GetName() => _product.Name;
}