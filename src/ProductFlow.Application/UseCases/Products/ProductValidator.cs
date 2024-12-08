using FluentValidation;
using ProductFlow.Communication.Requests;
using ProductFlow.Communication.Responses.Product;
using ProductFlow.Exception;

namespace ProductFlow.Application.UseCases.Products;

public class ProductValidator : AbstractValidator<RequestProductJson>
{
    public ProductValidator()
    {
        RuleFor(product => product.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_REQUIRED);
        RuleFor(product => product.Price).GreaterThan(0).WithMessage(ResourceErrorMessages.PRICE_MUST_BE_GREATER_THAN_ZERO);
        RuleFor(product => product.StockQuantity).GreaterThanOrEqualTo(0).WithMessage(ResourceErrorMessages.QUANTITY_STOCK_MUST_BE_GREATER_THAN_OR_EQUAL_ZERO);
        RuleFor(product => product.Status).IsInEnum().WithMessage(ResourceErrorMessages.STATUS_INVALID);
        RuleFor(product => product.Categories).ForEach(rule =>
        {
            rule.IsInEnum().WithMessage(ResourceErrorMessages.CATEGORY_TYPE_NOT_SUPPORTED);
        });
    }
}