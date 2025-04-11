using Microsoft.AspNetCore.Mvc;
using API_Sicily.DAL;
using API_Sicily.Service;
using API_Sicily.DTOs;
using API_Sicily.Models;

namespace API_Sicily.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TokenProvider _tokenProvider;

        // Initialisation du TokenProvider
        public AuthController(TokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        // Authentification du Client
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Vérif des champs
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { Message = "Email ou mot de passe manquant." });
            }

            try
            {
                // Récupérer le client par email
                ClientAuth? clientAuth = ClientDAO.GetClientByAuth(request.Email);

                // Si le mail est valide, on compare le hash de la bdd avec celui entré 
                if (clientAuth == null || !PasswordHash.VerifyPassword(request.Password, clientAuth.Mdp))
                {
                    return Unauthorized(new { Message = "Identifiants invalides." });
                }

                // Générer le token via l'instance injectée
                string token = _tokenProvider.GenerateToken(clientAuth);

                // Réponse de succès
                return Ok(new
                {
                    Token = token,
                    Message = $"Connexion réussie, Bienvenue.",
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