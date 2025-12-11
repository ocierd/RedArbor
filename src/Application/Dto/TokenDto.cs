
namespace RedArbor.Application.Dto;

public class TokenDto
{
    public string Token { get; set; } = null!;


    public double ExpiresIn { get; set; }


    public string RefreshToken { get; set; } = null!;


    public double RefreshTokenExpiresIn { get; set; }
}