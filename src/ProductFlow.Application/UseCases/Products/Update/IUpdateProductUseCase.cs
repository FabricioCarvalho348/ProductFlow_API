using ProductFlow.Communication.Requests;

namespace ProductFlow.Application.UseCases.Products.Update;

public interface IUpdateProductUseCase
{
    Task Execute(long id, RequestProductJson request);
}