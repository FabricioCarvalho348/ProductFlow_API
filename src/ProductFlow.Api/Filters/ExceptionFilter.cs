using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductFlow.Communication.Responses.Error;
using ProductFlow.Exception;
using ProductFlow.Exception.ExceptionBase;

namespace ProductFlow.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ProductFlowException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnknownError(context);
        }
    }

    private void HandleProjectException(ExceptionContext context)
    {
        var cashFlowException = (ProductFlowException)context.Exception;
        var errorResponse = new ResponseErrorJson(cashFlowException.GetErrors());
        
        context.HttpContext.Response.StatusCode = cashFlowException.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }

    private void ThrowUnknownError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOWN_VALUE);
        
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}