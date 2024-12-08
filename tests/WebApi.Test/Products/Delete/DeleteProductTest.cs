using System.Globalization;
using System.Net;
using System.Text.Json;
using FluentAssertions;
using ProductFlow.Exception;
using WebApi.Test.InlineData;

namespace WebApi.Test.Products.Delete;

public class DeleteProductTest : ProductFlowClassFixture
{
    private const string Method = "api/Products";

    private readonly string _token;
    private readonly long _productId;
    
    public DeleteProductTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _token = factory.UserManager.GetToken();
        _productId = factory.ProductManager.GetId();
    }
    
    [Fact]
    public async Task Success()
    {
        var result = await DoDelete(requestUri: $"{Method}/{_productId}", token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        
        result = await DoGet(requestUri: $"{Method}/{_productId}", token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Expense_Not_Found(string culture)
    {
        var result = await DoDelete(requestUri: $"{Method}/1000", token: _token, culture: culture);

        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        
        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();
        
        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("PRODUCT_NOT_FOUND", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }
}