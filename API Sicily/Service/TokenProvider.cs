using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API_Sicily.Models;

namespace API_Sicily.Service
{
    public sealed class TokenProvider
    {
        private readonly IConfiguration _configuration;

        public TokenProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Client client)
        {
            string secretKey = _configuration["Jwt:Secret"]!;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Création des "claims" pour le token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, client.Email),
            };

            // Création du token JWT
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"] ?? "API_Sicily",
                audience: _configuration["Jwt:Audience"] ?? "API_Sicily",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}