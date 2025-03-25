using Microsoft.AspNetCore.Mvc;
using API_Sicily.Models;
using API_Sicily.DAL;

namespace API_Sicily.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { Message = "Email ou mot de passe manquant." });
            }

            try
            {
                // 1. R�cup�rer le client par email seulement (sans v�rifier le mdp)
                Client? client = ClientDAO.GetClientByEmail(request.Email);

                if (client == null)
                {
                    return Unauthorized(new { Message = "Identifiants Invalides" });
                }

                // 2. V�rifier le mot de passe avec le hash stock�
                if (!PasswordHash.VerifyPassword(request.Password, client.Mdp))
                {
                    return Unauthorized(new { Message = "Mot de passe incorrect." });
                }

                // 3. Connexion r�ussie
                return Ok(new
                {
                    Message = "Connexion r�ussie.",
                    Client = new
                    {
                        client.IdClient,
                        client.Nom,
                        client.Prenom,
                        client.Email,
                        client.Cp,
                        client.Adresse,
                        client.Ville
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erreur interne du serveur.", Error = ex.Message });
            }
        }
    }
}
