using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CatalogoZap.Infrastructure.JWT;

public interface ITokenService
{
	string GenerateToken(Guid userId);
}

public class TokenService : ITokenService
{
	private readonly IConfiguration _config;

	public TokenService(IConfiguration config)
	{
		_config = config;
	}

	public static TokenValidationParameters GetValidationParameters(IConfiguration config) => new()
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(config["Jwt:Key"]!)
		),

		ValidateIssuer = true,
		ValidateAudience = true,

		ValidIssuer = config["Jwt:Issuer"],
		ValidAudience = config["Jwt:Audience"],

		ValidateLifetime = true,
		ClockSkew = TimeSpan.Zero
	};

	public string GenerateToken(Guid userId)
	{
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
		var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var claims = new[] {
			new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};

		var token = new JwtSecurityToken(
			issuer: _config["Jwt:Issuer"],
			audience: _config["Jwt:Audience"],
			claims: claims,
			expires: DateTime.UtcNow.AddHours(1),
			signingCredentials: credentials
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}

    public static Guid GetUserId(ClaimsPrincipal User)
    {
        return Guid.Parse(User.FindFirst(JwtRegisteredClaimNames.Sub)!.Value);
    }
}
