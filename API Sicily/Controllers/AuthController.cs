using Microsoft.AspNetCore.Mvc;
using API_Sicily.Models;
using API_Sicily.DAL;
using API_Sicily.Service;

namespace API_Sicily.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TokenProvider _tokenProvider;

        // Injection du TokenProvider
        public AuthController(TokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { Message = "Email ou mot de passe manquant." });
            }

            try
            {
                // 1. Récupérer le client par email
                Client? client = ClientDAO.GetClientByEmail(request.Email);

                if (client == null || !PasswordHash.VerifyPassword(request.Password, client.Mdp))
                {
                    return Unauthorized(new { Message = "Identifiants invalides ou mot de passe incorrect." });
                }

                // 2. Générer le token via l'instance injectée
                string token = _tokenProvider.GenerateToken(client);

                // 3. Réponse de succès
                return Ok(new
                {
                    Token = token,
                    Message = $"Connexion réussie! Bonjour {client.Prenom}",
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Erreur interne du serveur.",
                    Error = ex.Message
                });
            }
        }
    }
}