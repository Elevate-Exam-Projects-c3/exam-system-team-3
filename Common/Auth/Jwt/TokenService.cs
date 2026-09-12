using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using exam_system.Common.Enums;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace exam_system.Common.Auth.Jwt;



public class TokenService(IOptions<JwtOptions> options) : ITokenService

{
    
    private readonly JwtOptions  _options= options.Value;
    public string GenerateAccessToken(Guid userId, string email, UserRole role)
    {

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(ClaimTypes.Email, email),
            new(ClaimTypes.Role, role.ToString())

        };
        
        var key= new SymmetricSecurityKey(Convert.FromBase64String(_options.SecretKey));
        
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        
        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_options.AccessTokenExpiryMinutes),
            signingCredentials: credentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);

    }

    public string GenerateRefreshToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);
        
        return Convert.ToBase64String(randomBytes);
        
    }
}