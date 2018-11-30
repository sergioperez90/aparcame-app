using System;
using Newtonsoft.Json;

namespace aparcame.Models
{
    public class Usuario
    {

        [JsonProperty("id_usuario")]
        public int id_usuario { get; set; }

        [JsonProperty("nombre_usuario")]
        public string nombre_usuario { get; set; }

        [JsonProperty("apellidos_usuario")]
        public string apellidos_usuario { get; set; }

        [JsonProperty("email_usuario")]
        public string email_usuario { get; set; }

        [JsonProperty("foto_usuario")]
        public string foto_usuario { get; set; }

        [JsonProperty("direccion_usuario")]
        public string direccion_usuario { get; set; }

        [JsonProperty("cp_usuario")]
        public string cp_usuario { get; set; }

        [JsonProperty("localidad_usuario")]
        public string localidad_usuario { get; set; }

        [JsonProperty("provincia_usuario")]
        public string provincia_usuario { get; set; }

        [JsonProperty("puntos_usuario")]
        public int puntos_usuario { get; set; }

        [JsonProperty("lat_aparcado_usuario")]
        public double lat_aparcado_usuario { get; set; }

        [JsonProperty("long_aparcado_usuario")]
        public double long_aparcado_usuario { get; set; }

        [JsonProperty("fecha_registro_usuario")]
        public DateTime fecha_registro_usuario { get; set; }

        [JsonProperty("estado_usuario")]
        public int estado_usuario { get; set; }

        [JsonProperty("Token")]
        public string Token { get; set; }
    }


}
