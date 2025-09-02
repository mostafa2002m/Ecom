using Application.Interfaces.Authentication;
using Domain.Entities.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Repositories.Authentication
{
    public class JwTokenGenerator : IJwTokenGenerator
    {
        private readonly Jwt _jwt;
        public JwTokenGenerator(IOptions<Jwt> jwt)
        {
            _jwt = jwt.Value;
        }
        public async Task<string> GenerateToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.securityKey!));
            var credintial = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(
               issuer: _jwt.Issuer,
               audience: _jwt.Audience,
               signingCredentials: credintial,
               claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes));

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return await Task.FromResult(tokenString);



        }
    }
}
