using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SicilyLinesMobile.Models
{
    public class ReservationModel
    {
        [JsonProperty("nomTraversee")]
        public string? NomTraversee { get; set; }

        [JsonProperty("dateDepart")]
        public DateTime DateDepart { get; set; }
    }
}

