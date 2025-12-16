namespace RedArbor.Application.Common.Exceptions;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException() : base("No access granted.")
    {
    }
}