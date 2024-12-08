using Bogus;
using CommonTestUtilities.Cryptography;
using ProductFlow.Domain.Entities;
using ProductFlow.Domain.Enums;

namespace CommonTestUtilities.Entities;

public class UserBuilder
{
    public static User Build(string role = Roles.Member)
    {
        var passwordEncrypter = new PasswordEncrypterBuilder().Build();

        var user = new Faker<User>()
            .RuleFor(u => u.Id, _ => 1)
            .RuleFor(u => u.Name, faker => faker.Person.FirstName)
            .RuleFor(u => u.Email, (faker, user) => faker.Internet.Email(user.Name))
            .RuleFor(u => u.Password, (_, user) => passwordEncrypter.Encrypt(user.Password))
            .RuleFor(u => u.UserIdentifier, _ => Guid.NewGuid())
            .RuleFor(u => u.Role, _ => role);

        return user;
    }
}