using Newtonsoft.Json;

namespace SicilyLinesMobile.DTOs.Responses
{
    public class ClientResponse
    {

        [JsonProperty("nom")]
        public string? Nom { get; set; }

        [JsonProperty("prenom")]
        public string? Prenom { get; set; }

        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("adresse")]
        public string? Adresse { get; set; }

        [JsonProperty("cp")]
        public string? Cp { get; set; }

        [JsonProperty("ville")]
        public string? Ville { get; set; }
    }

}

