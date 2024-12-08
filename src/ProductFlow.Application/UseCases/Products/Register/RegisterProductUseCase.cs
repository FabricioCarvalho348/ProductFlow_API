using AutoMapper;
using ProductFlow.Communication.Requests;
using ProductFlow.Communication.Responses.Product;
using ProductFlow.Domain.Entities;
using ProductFlow.Domain.Repositories;
using ProductFlow.Domain.Repositories.Products;
using ProductFlow.Exception.ExceptionBase;

namespace ProductFlow.Application.UseCases.Products.Register;

public class RegisterProductUseCase : IRegisterProductUseCase
{
    private readonly IProductsWriteOnlyRepository _productsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public RegisterProductUseCase(
        IProductsWriteOnlyRepository repository, 
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _productsRepository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<ResponseRegisteredProductJson> Execute(RequestProductJson request)
    {
        Validate(request);
        
        var product = _mapper.Map<Product>(request);
        
        await _productsRepository.Add(product);
        
        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisteredProductJson>(product);
    }

    private void Validate(RequestProductJson request)
    {
        var validator = new ProductValidator();
        
        var result = validator.Validate(request);


        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}