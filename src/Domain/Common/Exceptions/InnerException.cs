namespace Domain.Common.Exceptions;

public class InnerException : Exception
{
    public InnerException( int code, string message) : base(message)
    {
        Data.Add("Code", code);
    }
}