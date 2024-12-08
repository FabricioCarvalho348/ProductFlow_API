using ProductFlow.Communication.Responses.Product;

namespace ProductFlow.Application.UseCases.Products.GetById;

public interface IGetProductByIdUseCase
{
    Task<ResponseProductJson> Execute(long id);
}