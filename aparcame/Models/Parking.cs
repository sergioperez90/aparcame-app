using System;
using Newtonsoft.Json;

namespace aparcame.Models
{
    public class Parking
    {

        [JsonProperty("id_parking")]
        public int id_parking { get; set; }

        [JsonProperty("codigo_parking")]
        public string codigo_parking { get; set; }

        [JsonProperty("nombre_parking")]
        public string nombre_parking { get; set; }

        [JsonProperty("imagen_parking")]
        public string imagen_parking { get; set; }

        [JsonProperty("latitud_parking")]
        public double latitud_parking { get; set; }

        [JsonProperty("longitud_parking")]
        public double longitud_parking { get; set; }

        [JsonProperty("cp_parking")]
        public string cp_parking { get; set; }

        [JsonProperty("cant_usuario_normal_parking")]
        public int cant_usuario_normal_parking { get; set; }

        [JsonProperty("cant_usuario_minus_parking")]
        public int cant_usuario_minus_parking { get; set; }

        [JsonProperty("cant_usuario_energ_parking")]
        public int cant_usuario_energ_parking { get; set; }

        [JsonProperty("normal_total_parking")]
        public int normal_total_parking { get; set; }

        [JsonProperty("normal_disp_parking")]
        public int normal_disp_parking { get; set; }

        [JsonProperty("minus_total_parking")]
        public int minus_total_parking { get; set; }

        [JsonProperty("minus_disp_parking")]
        public int minus_disp_parking { get; set; }

        [JsonProperty("energ_total_parking")]
        public int energ_total_parking { get; set; }

        [JsonProperty("energ_disp_parking")]
        public int energ_disp_parking { get; set; }

        [JsonProperty("estado_parking")]
        public int estado_parking { get; set; }

        [JsonProperty("distancia")]
        public double distancia { get; set; }
    }
}
