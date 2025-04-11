using API_Sicily.DAL;
using API_Sicily.DTOs;
using API_Sicily.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace API_Sicily.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        // Récupérer les informations d'un client par l'id extrait du token
        [HttpGet("get")]
        public ActionResult<Client> GetClientInfo()
        {
            try
            {
                // Extrait le token JWT de l'en-tête Authorization en supprimant le préfixe "Bearer ".
                string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                if (string.IsNullOrEmpty(token))
                    return BadRequest("Token d'authentification manquant");

                if (!int.TryParse(GetIdFromToken(token), out int id))
                    return BadRequest("ID client invalide dans le token");

                Client? client = ClientDAO.GetClientById(id);

                if (client == null)
                    return NotFound("Client non trouvé");

                return Ok(client);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur : {ex.Message}");
            }
        }

        // Mettre à jour les informations du client
        [HttpPut("update")]
        public ActionResult UpdateClientInfo([FromBody] UpdateClientRequest request)
        {
            try
            {
                string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                if (string.IsNullOrEmpty(token))
                    return BadRequest("Token d'authentification manquant");

                if (!int.TryParse(GetIdFromToken(token), out int id))
                    return BadRequest("ID client invalide dans le token");

                bool success = ClientDAO.UpdateClientInfoById(id, request.Adresse, request.Cp, request.Ville);

                if (!success)
                    return BadRequest("Échec de la mise à jour des informations");

                return Ok("Informations mises à jour avec succès");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur : {ex.Message}");
            }
        }

        // Afficher les réservations du client
        [HttpGet("reservations")]
        public ActionResult<List<Reservation>> GetClientReservations()
        {
            try
            {
                string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                if (string.IsNullOrEmpty(token))
                    return BadRequest("Token d'authentification manquant");

                if (!int.TryParse(GetIdFromToken(token), out int id))
                    return BadRequest("ID client invalide dans le token");

                List<Reservation> reservations = ClientDAO.GetReservationsById(id);

                if (reservations.Count == 0)
                    return NotFound("Aucune réservation trouvée pour ce client");

                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur : {ex.Message}");
            }
        }

        // Méthode pour extraire l'id du token JWT
        private string GetIdFromToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jsonToken != null)
                {
                    var idClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
                    if (idClaim != null)
                        return idClaim.Value;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'extraction de l'id du token : " + ex.Message);
            }

            return string.Empty;
        }
    }
}
