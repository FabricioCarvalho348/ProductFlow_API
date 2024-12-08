namespace ProductFlow.Exception.ExceptionBase;

public abstract class ProductFlowException : SystemException
{
    protected ProductFlowException(string message) : base(message)
    {
    }
    
    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}