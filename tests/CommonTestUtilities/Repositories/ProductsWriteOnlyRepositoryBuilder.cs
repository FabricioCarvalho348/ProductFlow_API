using Moq;
using ProductFlow.Domain.Repositories.Products;

namespace CommonTestUtilities.Repositories;

public class ProductsWriteOnlyRepositoryBuilder
{
    public static IProductsWriteOnlyRepository Build()
    {
        var mock = new Mock<IProductsWriteOnlyRepository>();

        return mock.Object;
    }
}