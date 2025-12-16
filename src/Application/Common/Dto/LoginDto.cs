namespace RedArbor.Application.Common.Dto;
/// <summary>
/// Data Transfer Object for user login
/// </summary>
public class LoginDto
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}