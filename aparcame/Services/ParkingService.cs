﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using aparcame.Models;
using Newtonsoft.Json;

namespace aparcame.Services
{
    public class ParkingService : IParkingService
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
        /// Metodo que obtiene los parkings
        /// </summary>
        /// <returns>The parkings.</returns>
        public async Task<List<Parking>> DameParkings()
        {
            try
            {
                //CREACIÓN URL ENDPOINT
                string _urlEndpoint = Constants.RestURL + "parking";

                //PETICÓN ENDPOINT
                var response = await API.GetAsync(_urlEndpoint);
                response.EnsureSuccessStatusCode();

                // CÓDIGOS 200
                var json = await response.Content.ReadAsStringAsync();
                var rootobject = JsonConvert.DeserializeObject<List<Parking>>(json);

                return rootobject;
            }
            catch (Exception e)
            {
                return new List<Parking>();
            }
        }

        /// <summary>
        /// Metodo que obtiene los parkings por id
        /// </summary>
        /// <returns>El parking por identificador.</returns>
        /// <param name="id">Identifier.</param>
        public async Task<Parking> DameParkingPorId(string id)
        {
            try
            {
                //CREACIÓN URL ENDPOINT
                string _urlEndpoint = Constants.RestURL + "parking?id="+id;
                Debug.WriteLine(_urlEndpoint);

                //PETICÓN ENDPOINT
                var response = await API.GetAsync(_urlEndpoint);
                response.EnsureSuccessStatusCode();

                // CÓDIGOS 200
                var json = await response.Content.ReadAsStringAsync();
                var rootobject = JsonConvert.DeserializeObject<List<Parking>>(json);

                return rootobject[0];
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Metodo que comprueba los parkings
        /// </summary>
        /// <returns>El parking.</returns>
        /// <param name="latitud">Latitud.</param>
        /// <param name="longitud">Longitud.</param>
        /// <param name="cp">Cp.</param>
		public async Task<int> ComprobarParking(double latitud, double longitud, string cp)
		{
			try
			{
                string lat = latitud.ToString().Replace(",", ".");
                string lon = longitud.ToString().Replace(",", ".");
				//CREACIÓN URL ENDPOINT
				string _urlEndpoint = Constants.RestURL + "parking/comprobar";
                _urlEndpoint += "?latitud=" + lat;
                _urlEndpoint += "&longitud=" + lon;
                _urlEndpoint += "&cp=" + cp;
                Debug.WriteLine(_urlEndpoint);
				//PETICÓN ENDPOINT
				var response = await API.GetAsync(_urlEndpoint);
				response.EnsureSuccessStatusCode();

				var json = await response.Content.ReadAsStringAsync();
				var rootobject = JsonConvert.DeserializeObject<Parking>(json);

                return rootobject.id_parking;
			}
			catch (Exception e)
			{
                return -1;
			}
		}

        /// <summary>
        /// Metodo que resta el sitio
        /// </summary>
        /// <returns>El sitio.</returns>
        /// <param name="id">Identifier.</param>
        public async Task<bool> RestarSitio(int id)
        {
			try
			{
			
				//CREACIÓN URL ENDPOINT
				string _urlEndpoint = Constants.RestURL + "parking/restarsitio";

				Debug.WriteLine(_urlEndpoint);

                var values = new Dictionary<string, string>
                {
                    {"id", id.ToString()}
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

        /// <summary>
        /// Metodo que suma el sitio
        /// </summary>
        /// <returns>El sitio restado.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="tipo">Tipo.</param>
        public async Task<bool> SumarSitio(int id, int tipo)
        {
            try
            {
                //CREACIÓN URL ENDPOINT
                string _urlEndpoint = Constants.RestURL + "parking/sumarsitio";

                Debug.WriteLine(_urlEndpoint);

                var values = new Dictionary<string, string>
                {
                    {"id", id.ToString()},
                    {"tipoVehiculo", tipo.ToString()}
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
