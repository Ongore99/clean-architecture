namespace Domain.Common.Exceptions;

public class InnerException : Exception
{
    public InnerException(string message, int code) : base(message)
    {
        Data.Add("Code", code);
    }
}