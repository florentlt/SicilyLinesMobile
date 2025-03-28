using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API_Sicily.Models
{
    public static class JwtTokenGenerator
    {
        public static string GenerateToken(Client client, string secretKey)
        {
            // Clé de sécurité (clé secrète utilisée pour signer le token)
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Création des "claims" pour le token, tu peux ajouter d'autres claims si nécessaire
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, client.IdClient.ToString())
            };

            // Création du token JWT
            var token = new JwtSecurityToken(
                issuer: "API_Sicily",
                audience: "API_Sicily",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            // Retourner le token sous forme de chaîne
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
