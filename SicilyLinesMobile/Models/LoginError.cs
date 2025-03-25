using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SicilyLinesMobile.Models
{
    public class LoginError
    {
        [JsonProperty("error")]
        public string? Error { get; set; }  // Nullable string
    }


}

