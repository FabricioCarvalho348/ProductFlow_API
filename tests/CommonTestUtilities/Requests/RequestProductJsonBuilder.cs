using Bogus;
using ProductFlow.Communication.Enums;
using ProductFlow.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class RequestProductJsonBuilder
{
    public static RequestProductJson Build()
    {
        return new Faker<RequestProductJson>()
            .RuleFor(u => u.Name, faker => faker.Commerce.ProductName())
            .RuleFor(r => r.Price, faker => faker.Random.Decimal(min: 1, max: 1000))
            .RuleFor(r => r.Status, faker => faker.PickRandom<Status>())
            .RuleFor(r => r.Categories, faker => faker.Make(1 , faker.PickRandom<Category>));
    }
}