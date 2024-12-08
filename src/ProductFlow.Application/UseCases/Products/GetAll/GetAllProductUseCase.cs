using AutoMapper;
using ProductFlow.Communication.Responses.Product;
using ProductFlow.Domain.Repositories.Products;

namespace ProductFlow.Application.UseCases.Products.GetAll;

public class GetAllProductUseCase : IGetAllProductUseCase
{
    private readonly IProductsReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    // private readonly ILoggedUser _loggedUser;

    public GetAllProductUseCase(IProductsReadOnlyRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ResponseProductsJson> Execute()
    {
        var result = await _repository.GetAll();

        return new ResponseProductsJson
        {
            Products = _mapper.Map<List<ResponseShortProductJson>>(result)
        };
    }
}