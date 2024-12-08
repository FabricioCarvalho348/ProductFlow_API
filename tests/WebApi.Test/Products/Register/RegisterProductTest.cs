using System.Globalization;
using System.Net;
using System.Text.Json;
using CommonTestUtilities.Requests;
using FluentAssertions;
using ProductFlow.Exception;
using WebApi.Test.InlineData;

namespace WebApi.Test.Products.Register;

public class RegisterProductTest : ProductFlowClassFixture
{
    private const string Method = "api/Products";

    private readonly string _token;

    public RegisterProductTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.UserManager.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestProductJsonBuilder.Build();
        
        var result = await DoPost(requestUri: Method, request: request, token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.Created);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);
        
        response.RootElement.GetProperty("name").GetString().Should().Be(request.Name);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Name_Empty(string culture)
    {
        var request = RequestProductJsonBuilder.Build();
        request.Name = string.Empty;

        var result = await DoPost(requestUri: Method, request: request, token: _token, culture: culture);
        
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var body = await result.Content.ReadAsStreamAsync();
        
        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("NAME_REQUIRED", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }
}