using ProductFlow.Communication.Responses.Product;

namespace ProductFlow.Application.UseCases.Products.GetAll;

public interface IGetAllProductUseCase
{
    Task<ResponseProductsJson> Execute();
}