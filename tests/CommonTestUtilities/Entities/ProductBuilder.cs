using Bogus;
using ProductFlow.Domain.Entities;
using ProductFlow.Domain.Enums;

namespace CommonTestUtilities.Entities;

public class ProductBuilder
{
    public static List<Product> Collection(uint count = 2)
    {
        var list = new List<Product>();

        if (count == 0)
            count = 1;

        var productId = 1;

        for (int i = 0; i < count; i++)
        {
            var product = Build();
            product.Id = productId++;

            list.Add(product);
        }

        return list;
    }
    
    public static List<Product> CollectionInStock(uint count = 8)
    {
        var list = new List<Product>();
        
        if (count == 0)
            count = 1;

        var productId = 1;

        for (int i = 0; i < count; i++)
        {
            var productUnavailable = Build();
            var product = Build();
            product.Id = productId++;
            
            product.Status = Status.InStock;
            productUnavailable.Status = Status.Unavailable;

            list.Add(product);
            list.Add(productUnavailable);
        }

        return list;
    }

    public static Product Build()
    {
        return new Faker<Product>()
            .RuleFor(u => u.Id, _ => 1)
            .RuleFor(u => u.Name, faker => faker.Commerce.ProductName())
            .RuleFor(r => r.Price, faker => faker.Random.Decimal(min: 1, max: 1000))
            .RuleFor(r => r.Status, faker => faker.PickRandom<Status>())
            .RuleFor(r => r.Categories, faker => faker.Make(1, () => new ProductFlow.Domain.Entities.Category
            {
                Id = 1,
                Value = faker.PickRandom<ProductFlow.Domain.Enums.Category>(),
                ProductId = 1
            }));
    }
    
    public static Product BuildForUsers(User user)
    {
        return new Faker<Product>()
            .RuleFor(u => u.Id, _ => 1)
            .RuleFor(u => u.Name, faker => faker.Commerce.ProductName())
            .RuleFor(r => r.Price, faker => faker.Random.Decimal(min: 1, max: 1000))
            .RuleFor(r => r.Status, faker => faker.PickRandom<Status>())
            .RuleFor(r => r.Categories, faker => faker.Make(1, () => new ProductFlow.Domain.Entities.Category
            {
                Id = 1,
                Value = faker.PickRandom<ProductFlow.Domain.Enums.Category>(),
                ProductId = 1
            }));
    }
}