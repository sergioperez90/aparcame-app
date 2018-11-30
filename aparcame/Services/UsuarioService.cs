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
