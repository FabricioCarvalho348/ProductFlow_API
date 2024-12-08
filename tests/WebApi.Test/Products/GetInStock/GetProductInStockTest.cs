using System.Net;
using System.Text.Json;
using FluentAssertions;

namespace WebApi.Test.Products.GetInStock;

public class GetProductInStockTest : ProductFlowClassFixture
{
    private const string Method = "api/Products/products-in-stock";
    private readonly string _token;
    
    public GetProductInStockTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.UserEmployee.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoGet(requestUri: Method, token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var body = await result.Content.ReadAsStreamAsync();
        
        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("products").EnumerateArray().Should().NotBeNullOrEmpty();
    }
}