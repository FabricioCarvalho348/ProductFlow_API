using ProductFlow.Domain.Repositories;
using ProductFlow.Domain.Repositories.Products;
using ProductFlow.Domain.Services.LoggedUser;
using ProductFlow.Exception;
using ProductFlow.Exception.ExceptionBase;

namespace ProductFlow.Application.UseCases.Products.Delete;

public class DeleteProductUseCase : IDeleteProductUseCase
{
    private readonly IProductsReadOnlyRepository _readOnlyRepository;
    private readonly IProductsWriteOnlyRepository _writeOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductUseCase(IProductsReadOnlyRepository readOnlyRepository,
        IProductsWriteOnlyRepository writeOnlyRepository,
        IUnitOfWork unitOfWork)
    {
        _readOnlyRepository = readOnlyRepository;
        _writeOnlyRepository = writeOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long id)
    {
        var expense = await _readOnlyRepository.GetById(id);
        
        if (expense is null)
        {
            throw new NotFoundException(ResourceErrorMessages.PRODUCT_NOT_FOUND);
        }
        
        await _writeOnlyRepository.Delete(id);
        
        await _unitOfWork.Commit();
    }
}