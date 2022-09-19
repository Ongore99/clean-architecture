namespace Domain.Common.Exceptions;

public class AuthorizationException : Exception
{
    public AuthorizationException(string message, int code) : base(message)
    {
        Data.Add("Code", code);
    }
}