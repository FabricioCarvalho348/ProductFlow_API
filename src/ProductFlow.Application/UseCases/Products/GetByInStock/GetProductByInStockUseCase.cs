using AutoMapper;
using ProductFlow.Communication.Responses.Product;
using ProductFlow.Domain.Repositories.Products;

namespace ProductFlow.Application.UseCases.Products.GetByInStock;

public class GetProductByInStockUseCase : IGetProductByInStockUseCase
{
    private readonly IProductsReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    
    public GetProductByInStockUseCase(IProductsReadOnlyRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ResponseProductsJson> Execute()
    {

        var result = await _repository.FilterProductInStock();

        return new ResponseProductsJson
        {
            Products = _mapper.Map<List<ResponseShortProductJson>>(result)
        };
    }
}