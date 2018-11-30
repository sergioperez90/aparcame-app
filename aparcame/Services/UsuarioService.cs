using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using aparcame.Models;
using Newtonsoft.Json;

namespace aparcame.Services
{
    public class UsuarioService : IUsuarioService
    {
        public HttpClient API
        {
            get
            {
                var httpClient = new HttpClient();

                return httpClient;
            }
        }

        /// <summary>
        /// Metodo que añade el vehiculo
        /// </summary>
        /// <returns>El vehiculo.</returns>
        /// <param name="tipo_vehiculo">Tipo vehiculo.</param>
        /// <param name="id_usuario">Identifier usuario.</param>
        public async Task<bool> AddVehiculo(int tipo_vehiculo, string id_usuario)
        {
            try
            {
                //CREACIÓN URL ENDPOINT
                string _urlEndpoint = Constants.RestURL + "usuario/vehiculo";

                Debug.WriteLine(_urlEndpoint);

                var values = new Dictionary<string, string>
                {
                    {"id_usuario", id_usuario},
                    {"tipo_vehiculo", tipo_vehiculo.ToString()}
                };

                var content = new FormUrlEncodedContent(values);

                //PETICÓN ENDPOINT
                var response = await API.PostAsync(_urlEndpoint, content);
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Metodo para obtener el veihiculo del usuario
        /// </summary>
        /// <returns>El vehiculo usuario.</returns>
        /// <param name="id_usuario">Identifier usuario.</param>
        public async Task<Vehiculo> DameVehiculoUsuario(string id_usuario)
        {
            try
            {            
                //CREACIÓN URL ENDPOINT
                string _urlEndpoint = Constants.RestURL + "usuario/vehiculo/?id="+id_usuario;

                //PETICÓN ENDPOINT
                var response = await API.GetAsync(_urlEndpoint);
                response.EnsureSuccessStatusCode();

                // CÓDIGOS 200
                var json = await response.Content.ReadAsStringAsync();
                var rootobject = JsonConvert.DeserializeObject<List<Vehiculo>>(json);

                //Guardamos el usuario en constantes
                if (rootobject != null)
                    Constants.vehiculo = rootobject[0];

                return rootobject[0];
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Metodo para hacer login
        /// </summary>
        /// <returns>El usuario.</returns>
        /// <param name="email">Email.</param>
        /// <param name="pass">Pass.</param>
        public async Task<Usuario> Login(string email, string pass)
        {
            try
            {
                //CREACIÓN URL ENDPOINT
                string _urlEndpoint = Constants.RestURL + "usuario/login";

                Debug.WriteLine(_urlEndpoint);

                var values = new Dictionary<string, string>
                {
                    {"email_usuario", email},
                    {"pass_usuario", pass}
                };

                var content = new FormUrlEncodedContent(values);

                //PETICÓN ENDPOINT
                var response = await API.PostAsync(_urlEndpoint, content);
                response.EnsureSuccessStatusCode();

                // CÓDIGOS 200
                var json = await response.Content.ReadAsStringAsync();
                var rootobject = JsonConvert.DeserializeObject<Usuario>(json);

                //Guardamos el usuario en constantes
                if(rootobject != null)
                    Constants.usuario = rootobject;

                return rootobject;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Metodo para el registro
        /// </summary>
        /// <returns>Verdadero o falso.</returns>
        /// <param name="nombre">Nombre.</param>
        /// <param name="email">Email.</param>
        /// <param name="pass">Pass.</param>
        public async Task<bool> Registro(string nombre, string email, string pass)
        {
            try
            {
                //CREACIÓN URL ENDPOINT
                string _urlEndpoint = Constants.RestURL + "usuario/registro";

                Debug.WriteLine(_urlEndpoint);

                var values = new Dictionary<string, string>
                {
                    {"nombre_usuario", nombre},
                    {"email_usuario", email},
                    {"contra_usuario", pass}
                };

                var content = new FormUrlEncodedContent(values);

                //PETICÓN ENDPOINT
                var response = await API.PostAsync(_urlEndpoint, content);
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Metodo para sumar el sitio
        /// </summary>
        /// <returns>Verdadero o falso</returns>
        /// <param name="puntos">Puntos.</param>
        /// <param name="id_usuario">Identifier usuario.</param>
        public async Task<bool> SumarPuntos(string puntos, string id_usuario)
        {
            try
            {
                //CREACIÓN URL ENDPOINT
                string _urlEndpoint = Constants.RestURL + "usuario";

                Debug.WriteLine(_urlEndpoint);

                var values = new Dictionary<string, string>
                {
                    {"puntos_usuario", puntos},
                    {"id_usuario", id_usuario},                  
                };

                var content = new FormUrlEncodedContent(values);

                //PETICÓN ENDPOINT
                var response = await API.PutAsync(_urlEndpoint, content);
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
