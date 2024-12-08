using System.Net;

namespace ProductFlow.Exception.ExceptionBase;

public class NotFoundException : ProductFlowException
{
    public NotFoundException(string message) : base(message)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.NotFound;
    public override List<string> GetErrors() => [Message];
}