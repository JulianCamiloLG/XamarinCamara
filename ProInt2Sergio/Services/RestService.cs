using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using ProInt2Sergio.Models;
using System.Diagnostics;

namespace ProInt2Sergio.Services
{
    class RestService
    {
        HttpClient _cliente;

        public RestService()
        {
            _cliente = new HttpClient();
        }

        public async Task<Prediccion> PostImagenPredecir(string URI, string json)
        {
            string respuesta = String.Empty;
            Prediccion prediccion = new Prediccion();
            try
            {
                StringContent jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
                Console.WriteLine(json);
                HttpResponseMessage response = await _cliente.PostAsync(URI, jsonContent);
                respuesta = await response.Content.ReadAsStringAsync();
                prediccion = JsonConvert.DeserializeObject<Prediccion>(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Not succeded");
                Debug.WriteLine(ex.Message);
            }
            return prediccion;
        }
    }
}
