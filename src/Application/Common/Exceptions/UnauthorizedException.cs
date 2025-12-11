
namespace RedArbor.Application.Common.Exceptions;

/// <summary>
/// Exception thrown for unauthorized access
/// </summary>
public class UnauthorizedException(string message = "Unauthorized access") 
: Exception(message)
{
}