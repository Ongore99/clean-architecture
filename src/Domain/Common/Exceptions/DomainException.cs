namespace Domain.Common.Exceptions;

public abstract class DomainException : Exception 
{
    public DomainException(string message, int code) : base(message)
    {
        Data.Add("Code", code);
    }
}