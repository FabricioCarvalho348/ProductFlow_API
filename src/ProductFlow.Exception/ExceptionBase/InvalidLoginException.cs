using System.Net;

namespace ProductFlow.Exception.ExceptionBase;

public class InvalidLoginException : ProductFlowException
{
    public InvalidLoginException() : base(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID)
    {
    }
    
    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}