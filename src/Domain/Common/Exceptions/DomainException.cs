namespace Domain.Common.Exceptions;

public abstract class DomainException : Exception 
{
    public int Code { get; set; }
    
    public string Message { get; set; }

    public DomainException(string message, int code) : base(message)
    {
        Code = code;
        Data.Add("Code", code);
    }
}