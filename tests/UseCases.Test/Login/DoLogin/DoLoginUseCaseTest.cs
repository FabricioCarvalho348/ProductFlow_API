﻿using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Token;
using FluentAssertions;
using ProductFlow.Application.UseCases.Login.DoLogin;
using ProductFlow.Domain.Entities;
using ProductFlow.Exception;
using ProductFlow.Exception.ExceptionBase;

namespace UseCases.Test.Login.DoLogin;

public class DoLoginUseCaseTest
{
        [Fact]
    public async Task Success()
    {
        // Arrange
        var user = UserBuilder.Build();
        var request = RequestLoginJsonBuilder.Build();
        request.Email = user.Email;
        var useCase = CreateUseCase(user, request.Password);

        // Act
        var result = await useCase.Execute(request);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(user.Name);
        result.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Error_User_Not_Found()
    {
        var user = UserBuilder.Build();
        var request = RequestLoginJsonBuilder.Build();

        var useCase = CreateUseCase(user, request.Password);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<InvalidLoginException>();

        result.Where(ex =>
            ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID));
    }

    [Fact]
    public async Task Error_Password_Not_Match()
    {
        // Arrange
        var user = UserBuilder.Build();
        var request = RequestLoginJsonBuilder.Build();
        request.Email = user.Email;
        var useCase = CreateUseCase(user);

        // Act
        var act = async () => await useCase.Execute(request);

        // Assert
        var result = await act.Should().ThrowAsync<InvalidLoginException>();
        result.Where(ex =>
            ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID));
    }

    private DoLoginUseCase CreateUseCase(User user, string? password = null)
    {
        var passwordEncrypter = new PasswordEncrypterBuilder().Verify(password).Build();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();
        var readRepository = new UserReadOnlyRepositoryBuilder().GetUserByEmail(user).Build();

        return new DoLoginUseCase(readRepository, passwordEncrypter, tokenGenerator);
    }
}