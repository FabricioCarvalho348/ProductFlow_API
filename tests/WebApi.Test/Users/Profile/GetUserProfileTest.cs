﻿using System.Net;
using System.Text.Json;
using FluentAssertions;

namespace WebApi.Test.Users.Profile;

public class GetUserProfileTest : ProductFlowClassFixture
{
    private const string Method = "api/User";

    private readonly string _token;
    private readonly string _userName;
    private readonly string _userEmail;
    
    public GetUserProfileTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.UserMember.GetToken();
        _userName = webApplicationFactory.UserMember.GetName();
        _userEmail = webApplicationFactory.UserMember.GetEmail();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoGet(Method, _token);

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("name").GetString().Should().Be(_userName);
        response.RootElement.GetProperty("email").GetString().Should().Be(_userEmail);
    }
}