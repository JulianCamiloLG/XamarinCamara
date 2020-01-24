using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ProInt2Sergio.Models
{
    class Predecir
    {
        [JsonProperty("nombreImagen")]
        public string nombreImagen { set; get; }

        [JsonProperty("imagen")]
        public string imagen { set; get; }
    }
}
