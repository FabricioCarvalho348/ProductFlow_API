namespace ProductFlow.Application.UseCases.Products.Delete;

public interface IDeleteProductUseCase
{
    Task Execute(long id);
}