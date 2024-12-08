using System.Globalization;
using System.Net;
using System.Text.Json;
using CommonTestUtilities.Requests;
using FluentAssertions;
using ProductFlow.Exception;
using WebApi.Test.InlineData;

namespace WebApi.Test.Products.Update;

public class UpdateProductTest : ProductFlowClassFixture
{
        private const string Method = "api/Products";

    private readonly string _token;
    private readonly long _productId;

    public UpdateProductTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.UserManager.GetToken();
        _productId = webApplicationFactory.ProductManager.GetId();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestProductJsonBuilder.Build();
        
        var result = await DoPut(requestUri: $"{Method}/{_productId}", request: request, token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Name_Empty(string culture)
    {
        var request = RequestProductJsonBuilder.Build();
        request.Name = string.Empty;
        
        var result = await DoPut(requestUri: $"{Method}/{_productId}", request: request, token: _token, culture: culture);
        
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage =
            ResourceErrorMessages.ResourceManager.GetString("NAME_REQUIRED", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Product_Not_Found(string culture)
    {
        var request = RequestProductJsonBuilder.Build();

        var result = await DoPut(requestUri: $"{Method}/1000", request: request, token: _token, culture: culture);

        result.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage =
            ResourceErrorMessages.ResourceManager.GetString("PRODUCT_NOT_FOUND", new CultureInfo(culture));
        
        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }
}