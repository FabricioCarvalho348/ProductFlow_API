﻿using System.Globalization;
using System.Net;
using System.Text.Json;
using CommonTestUtilities.Requests;
using FluentAssertions;
using ProductFlow.Communication.Requests;
using ProductFlow.Exception;
using WebApi.Test.InlineData;

namespace WebApi.Test.Login.DoLogin;

public class DoLoginTest : ProductFlowClassFixture
{
    private const string Method = "api/Login";
    private readonly string _name;
    private readonly string _email;
    private readonly string _password;

    public DoLoginTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _name = webApplicationFactory.UserMember.GetName();
        _email = webApplicationFactory.UserMember.GetEmail();
        _password = webApplicationFactory.UserMember.GetPassword();
    }

    [Fact]
    public async Task Success()
    {
        // Arrange
        var request = new RequestLoginJson
        {
            Email = _email,
            Password = _password
        };
        
        var response = await DoPost(requestUri: Method, request: request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("name").GetString().Should().Be(_name);
        responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
    }
    
    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Login_Invalid(string culture)
    {
        var request = RequestLoginJsonBuilder.Build();
        
        var response = await DoPost(requestUri: Method, request: request, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("EMAIL_OR_PASSWORD_INVALID", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(c => c.GetString()!.Equals(expectedMessage));
    }
}