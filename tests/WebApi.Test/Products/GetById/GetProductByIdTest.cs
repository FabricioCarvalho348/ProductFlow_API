using System.Globalization;
using System.Net;
using System.Text.Json;
using FluentAssertions;
using ProductFlow.Communication.Enums;
using ProductFlow.Exception;
using WebApi.Test.InlineData;

namespace WebApi.Test.Products.GetById;

public class GetProductByIdTest : ProductFlowClassFixture
{
        private const string Method = "api/Products";

    private readonly string _token;
    private readonly long _productId;    

    public GetProductByIdTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.UserEmployee.GetToken();
        _productId = webApplicationFactory.ProductEmployee.GetId();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoGet(requestUri: $"{Method}/{_productId}", token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);
        
        response.RootElement.GetProperty("id").GetInt64().Should().Be(_productId);
        response.RootElement.GetProperty("name").GetString().Should().NotBeNullOrWhiteSpace();
        response.RootElement.GetProperty("price").GetDecimal().Should().BeGreaterThan(0);
        response.RootElement.GetProperty("categories").EnumerateArray().Should().NotBeNullOrEmpty();
        
        var status = response.RootElement.GetProperty("status").GetInt32();
        Enum.IsDefined(typeof(Status), status).Should().BeTrue();
    }
    
    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Product_Not_Found(string culture)
    {
        var result = await DoGet(requestUri: $"{Method}/1000", token: _token, culture: culture);
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        var body = await result.Content.ReadAsStreamAsync();
        var response = await JsonDocument.ParseAsync(body);
        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();
        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("PRODUCT_NOT_FOUND", new CultureInfo(culture));
        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }
}