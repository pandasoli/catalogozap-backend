using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CatalogoZap.Infrastructure.JWT;

public interface ITokenService
{
	string GenerateToken(string username);
	string? GetJWTByHeader(HttpContext context);
	string? decodeJWT(string token);
	string? GetJWTAndDecode(HttpContext context);

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

	public string GenerateToken(string username)
	{
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
		var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var claims = new[] {
			new Claim(JwtRegisteredClaimNames.Sub, username),
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

	public string? GetJWTByHeader(HttpContext context)
	{
		string? authHeader = context.Request.Headers["Authorization"];

		if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
		{
			return null;
		}

		string jwtToken = authHeader.Substring("Bearer ".Length).Trim();

		return jwtToken;
	}

	public string? decodeJWT(string token)
	{
		var handler = new JwtSecurityTokenHandler();

		if (!handler.CanReadToken(token))
		{
			return null;
		}

		var jwtToken = handler.ReadJwtToken(token);

		return jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
	}

	public string? GetJWTAndDecode(HttpContext context)
	{
		string? Jwt = GetJWTByHeader(context);
		if (string.IsNullOrEmpty(Jwt))
		{
			return null;
		}

		string? decodedJwt = decodeJWT(Jwt);
		if (string.IsNullOrEmpty(decodedJwt))
		{
			return null;
		}

		return decodedJwt;
	}
}
