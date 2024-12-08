using Moq;
using ProductFlow.Domain.Entities;
using ProductFlow.Domain.Services.LoggedUser;

namespace CommonTestUtilities.LoggedUser;

public class LoggedUserBuilder
{
    public static ILoggedUser Build(User user)
    {
        var mock = new Mock<ILoggedUser>();
        mock.Setup(loggedUser => loggedUser.Get()).ReturnsAsync(user);

        return mock.Object;
    }
}