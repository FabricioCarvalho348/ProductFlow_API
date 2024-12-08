using Microsoft.AspNetCore.Mvc;
using ProductFlow.Application.UseCases.Login.DoLogin;
using ProductFlow.Communication.Requests;
using ProductFlow.Communication.Responses.Error;
using ProductFlow.Communication.Responses.User;

namespace ProductFlow.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(
        [FromServices] IDoLoginUseCase useCase,
        [FromBody] RequestLoginJson request)
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}