using Application.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Identity
{
    public class JwtTokenGeneratorService : IJwtTokenGenerator
    {
        private readonly InfrastructureSettings infrastructureSettings;

        public JwtTokenGeneratorService(IOptions<InfrastructureSettings> infrastructureSettings)
        {
            this.infrastructureSettings = infrastructureSettings.Value;
        }

        public string GenerateToken(User user, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.infrastructureSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.Name,user.FirstName + " " + user.LastName),
                    new Claim(ClaimTypes.Role, role),
                    new Claim("TenantId", user.TenantId == null ? "" : user.TenantId.Value.ToString()),
                    new Claim("TenantName", user.Tenant?.Name == null ? "" : user.Tenant.Name)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }
    }
}
