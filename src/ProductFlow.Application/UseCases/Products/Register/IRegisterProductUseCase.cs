using ProductFlow.Communication.Requests;
using ProductFlow.Communication.Responses.Product;

namespace ProductFlow.Application.UseCases.Products.Register;

public interface IRegisterProductUseCase
{
    Task<ResponseRegisteredProductJson> Execute(RequestProductJson request);
}