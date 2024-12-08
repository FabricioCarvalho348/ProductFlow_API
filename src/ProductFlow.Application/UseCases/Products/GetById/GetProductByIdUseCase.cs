using AutoMapper;
using ProductFlow.Communication.Responses.Product;
using ProductFlow.Domain.Repositories.Products;
using ProductFlow.Exception;
using ProductFlow.Exception.ExceptionBase;

namespace ProductFlow.Application.UseCases.Products.GetById;

public class GetProductByIdUseCase : IGetProductByIdUseCase
{
    private readonly IProductsReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    // private readonly ILoggedUser _loggedUser;

    public GetProductByIdUseCase(
        IProductsReadOnlyRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;

    }

    public async Task<ResponseProductJson> Execute(long id)
    {
        // var loggedUser = await _loggedUser.Get();
        
        var result = await _repository.GetById(id);

        if (result is null)
        {
            throw new NotFoundException(ResourceErrorMessages.PRODUCT_NOT_FOUND);
        }
        
        return _mapper.Map<ResponseProductJson>(result);
    }
}