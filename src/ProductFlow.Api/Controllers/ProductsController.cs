using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductFlow.Application.UseCases.Products.Delete;
using ProductFlow.Application.UseCases.Products.GetAll;
using ProductFlow.Application.UseCases.Products.GetById;
using ProductFlow.Application.UseCases.Products.GetByInStock;
using ProductFlow.Application.UseCases.Products.Register;
using ProductFlow.Application.UseCases.Products.Update;
using ProductFlow.Communication.Requests;
using ProductFlow.Communication.Responses.Error;
using ProductFlow.Communication.Responses.Product;

namespace ProductFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredProductJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Manager,Employee")]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterProductUseCase useCase,
        [FromBody] RequestProductJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseProductsJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize]
    public async Task<IActionResult> GetAllProducts([FromServices] IGetAllProductUseCase useCase)
    {
        var response = await useCase.Execute();

        if (response.Products.Count != 0)
            return Ok(response);

        return NoContent();
    }
    
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseProductJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> GetById(
        [FromServices] IGetProductByIdUseCase useCase,
        [FromRoute] long id)
    {
        var response = await useCase.Execute(id);
        
        return Ok(response);
    }
    
    [HttpGet("products-in-stock")]
    [ProducesResponseType(typeof(ResponseProductsJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> GetProductsInStock(
        [FromServices] IGetProductByInStockUseCase useCase)
    {
        var products = await useCase.Execute();
        return Ok(products);
    }
    
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [Authorize(Roles = "Manager,Employee")]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateProductUseCase useCase,
        [FromRoute] long id,
        [FromBody] RequestProductJson request)
    {
        await useCase.Execute(id, request);

        return NoContent();
    }
    

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> Delete(
        [FromServices] IDeleteProductUseCase useCase,
        [FromRoute] long id)
    {
        await useCase.Execute(id);

        return NoContent();
    }
}
