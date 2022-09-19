namespace Domain.Common.Exceptions;

public class AuthenticationCustomException : Exception
{
    public AuthenticationCustomException(string message, int code) : base(message)
    {
        Data.Add("Code", code);
    }
}