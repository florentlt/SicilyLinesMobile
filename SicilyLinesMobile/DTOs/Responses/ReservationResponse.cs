using Newtonsoft.Json;

namespace SicilyLinesMobile.DTOs.Responses
{
    public class ReservationResponse
    {
        [JsonProperty("nomTraversee")]
        public string? NomTraversee { get; set; }

        [JsonProperty("dateDepart")]
        public DateTime DateDepart { get; set; }
    }
}

