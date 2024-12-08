using CommonTestUtilities.Requests;
using FluentAssertions;
using ProductFlow.Application.UseCases.Products;
using ProductFlow.Communication.Enums;
using ProductFlow.Exception;

namespace Validators.Tests.Products;

public class ProductValidatorTests
{
        [Fact]
    public void Success()
    {
        // Arrange
        var validator = new ProductValidator();
        var request = RequestProductJsonBuilder.Build();
        
        // Act
        var result = validator.Validate(request);
        
        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("              ")]
    public void Error_Name_Empty(string name)
    {
        // Arrange
        var validator = new ProductValidator();
        var request = RequestProductJsonBuilder.Build();
        request.Name = name;
        
        // Act
        var result = validator.Validate(request);
        
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.NAME_REQUIRED));
    }
    
    [Fact]
    public void Error_Status_Invalid()
    {
        // Arrange
        var validator = new ProductValidator();
        var request = RequestProductJsonBuilder.Build();
        request.Status = (Status)700;
        
        // Act
        var result = validator.Validate(request);
        
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.STATUS_INVALID));
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-2)]
    [InlineData(-7)]
    public void Error_Price_Invalid(decimal price)
    {
        // Arrange
        var validator = new ProductValidator();
        var request = RequestProductJsonBuilder.Build();
        request.Price = price;
        
        // Act
        var result = validator.Validate(request);
        
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PRICE_MUST_BE_GREATER_THAN_ZERO));
    }
    
    [Theory]
    [InlineData(-1)]
    [InlineData(-2)]
    [InlineData(-7)]
    public void Error_Stock_Quantity_Invalid(int quantityStock)
    {
        // Arrange
        var validator = new ProductValidator();
        var request = RequestProductJsonBuilder.Build();
        request.StockQuantity = quantityStock;
        
        // Act
        var result = validator.Validate(request);
        
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.QUANTITY_STOCK_MUST_BE_GREATER_THAN_OR_EQUAL_ZERO));
    }
    
    [Fact]
    public void Error_Tag_Invalid()
    {
        // Arrange
        var validator = new ProductValidator();
        var request = RequestProductJsonBuilder.Build();
        request.Categories.Add((Category)1000);
        
        // Act
        var result = validator.Validate(request);
        
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.CATEGORY_TYPE_NOT_SUPPORTED));
    }
}