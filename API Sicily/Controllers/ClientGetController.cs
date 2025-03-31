using API_Sicily.DAL;
using API_Sicily.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace API_Sicily.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        // Récupérer les informations d'un client par email
        [HttpGet("get")]
        public ActionResult<Client> GetClientInfo()
        {
            try
            {
                // Récupérer le token d'authentification dans l'en-tête de la requête
                string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                if (string.IsNullOrEmpty(token))
                {
                    return BadRequest("Token d'authentification manquant");
                }

                // Extraire l'email du token JWT
                string email = GetEmailFromToken(token);

                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest("Email non trouvé dans le token");
                }

                // Appel au DAO pour récupérer les informations du client
                Client? client = ClientDAO.GetClientInfoByEmail(email);

                // Si le client n'est pas trouvé
                if (client == null)
                {
                    return NotFound("Client non trouvé");
                }

                // Retourner les informations du client
                return Ok(client);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur : {ex.Message}");
            }
        }

        // Méthode pour extraire l'email du token JWT
        private string GetEmailFromToken(string token)
        {
            try
            {
                // Décode le token JWT
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                // Vérifie si le token est valide et contient les informations nécessaires
                if (jsonToken != null)
                {
                    var emailClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email);
                    if (emailClaim != null)
                    {
                        return emailClaim.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'extraction de l'email du token : " + ex.Message);
            }

            return string.Empty;
        }
    }
}
