using Newtonsoft.Json;
using System;

namespace ProInt2Sergio.Models
{
    class Prediccion
    {
        [JsonProperty("idImagen")]
        public string idImagen { set; get; }

        [JsonProperty("prediccion")]
        public string prediccion { set; get; }

        [JsonProperty("probabilidades")]
        public string probabilidades { set; get; }
    }
}
