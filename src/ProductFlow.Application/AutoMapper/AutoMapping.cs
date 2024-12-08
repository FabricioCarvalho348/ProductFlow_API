using AutoMapper;
using ProductFlow.Communication.Requests;
using ProductFlow.Communication.Responses.Product;
using ProductFlow.Communication.Responses.User;
using ProductFlow.Domain.Entities;

namespace ProductFlow.Application.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestRegisterUserJson, User>()
            .ForMember(dest => dest.Password, config => config.Ignore());
        
        CreateMap<RequestProductJson, Product>()
            .ForMember(dest => dest.Categories, config => config.MapFrom(source => source.Categories.Distinct()));

        CreateMap<Communication.Enums.Category, Category>()
            .ForMember(dest => dest.Value, config => config.MapFrom(source => source));
    }

    private void EntityToResponse()
    {
        CreateMap<Product, ResponseProductJson>()
            .ForMember(dest => dest.Categories, config => config.MapFrom(source => source.Categories.Select(category => category.Value)));
        
        CreateMap<Product, ResponseRegisteredProductJson>();
        CreateMap<Product, ResponseShortProductJson>();
        CreateMap<User, ResponseUserProfileJson>();
    }
}