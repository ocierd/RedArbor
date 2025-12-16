namespace RedArbor.Application.Common.Security;



/// <summary>
/// Attribute to specify required permission for authorization.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
public class AuthorizeAttribute : Attribute
{
    public AuthorizeAttribute() { }

    public string? Roles { get; set; }

    public string? Policy { get; set; }
}