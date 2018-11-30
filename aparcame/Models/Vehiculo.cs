using System;
using Newtonsoft.Json;

namespace aparcame.Models
{
    public class Vehiculo
    {

        [JsonProperty("id_vehiculo")]
        public int id_vehiculo { get; set; }

        [JsonProperty("id_usuario_vehiculo")]
        public int id_usuario_vehiculo { get; set; }

        [JsonProperty("tipo_vehiculo")]
        public int tipo_vehiculo { get; set; }

        [JsonProperty("marca_vehiculo")]
        public string marca_vehiculo { get; set; }

        [JsonProperty("modelo_vehiculo")]
        public string modelo_vehiculo { get; set; }

        [JsonProperty("combustible_vehiculo")]
        public string combustible_vehiculo { get; set; }
    }
}
