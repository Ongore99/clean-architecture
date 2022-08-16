namespace Domain.Common.Exceptions;

public class AuthenticationCustomException : Exception
{
    public AuthenticationCustomException(string message) : base(message) { }
}