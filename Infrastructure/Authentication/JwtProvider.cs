using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Abstractions;
using Domain.Entities.Users;
using Domain.Primitives.Maybe;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication;

internal sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;

    public JwtProvider(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public string GetJwtToken(User user)
    {
        var claims = new Claim[]
        {
            new (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new (JwtRegisteredClaimNames.Name, user.UserName!),
            new (JwtRegisteredClaimNames.Email, user.Email!),

        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            null,  
            DateTime.Now.AddDays(7),
            signingCredentials);

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;

    }

    public Maybe<Guid> GetUserIdAsync(ClaimsPrincipal principal)
    {
        var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null) { return Maybe<Guid>.None; }

        return Maybe<Guid>.From(Guid.Parse(userIdClaim.Value));
    }
}