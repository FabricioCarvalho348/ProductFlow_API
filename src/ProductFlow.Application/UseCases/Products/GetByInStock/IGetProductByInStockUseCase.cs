using ProductFlow.Communication.Responses.Product;

namespace ProductFlow.Application.UseCases.Products.GetByInStock;

public interface IGetProductByInStockUseCase
{
    Task<ResponseProductsJson> Execute();
}