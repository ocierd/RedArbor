namespace RedArbor.Application.Common.Exceptions;

public class ValidationException : Exception
{

    public readonly List<string> Errors = [];


    public ValidationException(string message) : base(message)
    {

    }

    public ValidationException(IEnumerable<string> errors)
    : base("One or more validation failures have ocurred")
    {
        Errors.AddRange(errors);
    }

}