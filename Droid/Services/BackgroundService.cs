using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Util;
using aparcame.Services;
using Java.Lang;
using Java.Util;
using System.Timers;
using aparcame.Droid.Activities;
using System.Collections.Generic;

namespace aparcame.Droid.Services

{
	[Service(Exported = true, Name = "com.aparcame.aparcame.BackgroundService")]
	public class BackgroundService : Service, ILocationListener
    {
        private int id_parking = -1;
        double latitude, longitude;
        LocationManager locationManager;
        private static Handler mHandler = new Handler();
        private Java.Util.Timer mTimer = null;
        long notify_interval = 3000;
        public static string str_receiver = "com.aparcame.aparcame.BackgroundService";
        Intent intent;
        private IParkingService _parkingService;
        private IUsuarioService _usuarioService;
        private bool velocidadSuperior = false;
        private bool velocidadInferior = false;
        private static System.Timers.Timer temporizador;
        private static bool temporizadorActivo = false;
        private double latitudAparcado, longitudAparcado;
        private bool hasAparcado = false; //Con esta variable controlamos que no sume mas de un sitio, ya que el temporizador es mas rapido que llamar a la api
        private float VELOCIDADINFERIORPRUEBA = 0.2f;

        //Funciona
        Location location = null;
        bool isGPSEnable, isNetworkEnable;
       
        public override void OnCreate()
        {
            base.OnCreate();

            _parkingService = new ParkingService();
            _usuarioService = new UsuarioService();

            var _timer = new TimerTaskToGetLocation();

            temporizador = new System.Timers.Timer();
            temporizador.Interval = 10000; //Cambiar a 2 o cinco minutos
            temporizador.AutoReset = false;

			//Aqui habria que actualizar cada cierto tiempo
			mTimer = new Java.Util.Timer();
            mTimer.Schedule(_timer, 5, notify_interval); //actualizacion
            intent = new Intent(str_receiver);

            _timer._eventHandler += delegate {
                if(!hasAparcado) fn_getlocation();
            };


		}

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public void OnLocationChanged(Location location)
        {
           
        }

        public void OnProviderDisabled(string provider)
        {
          
        }

        public void OnProviderEnabled(string provider)
        {
           
        }

		
        //Metodo para la localizacion NO VA
        public void Geolocation()
        {
            locationManager = (LocationManager)ApplicationContext.GetSystemService(LocationService);
            IList<string> providers = locationManager.GetProviders(true);
            Location bestLocation = null;

            foreach (string provider in providers)
            {
                Location l = locationManager.GetLastKnownLocation(provider);
                if (l == null)
                {
                    continue;
                }
                if (bestLocation == null || l.Accuracy < bestLocation.Accuracy)
                {
                    Console.WriteLine("latitude " + l.Latitude + "");
                    Console.WriteLine("longitude " + l.Longitude + "");
                    Console.WriteLine("velocity " + l.Speed + "");

                    latitude = l.Latitude;
                    longitude = l.Longitude;

                    float velocidad = 3.5f; //Para pruebas
                    ComprobarVelocidad(l.Speed, l.Latitude, l.Longitude);

                    //Se lo paso a update
                    //SendToMain(l);
                }
            }
        }

        //Metodo para comprobar la velocidad
        public async void ComprobarVelocidad(float velocidad, double latitud, double longitud){

            if (velocidad > 2.5){
                velocidadSuperior = true;
            }

            //Si ha superado una velocidad a 2.5
            if(velocidadSuperior){
                Console.WriteLine("Has tenido una velocidad superior a 2.5");

                MostrarAlertaConduciendo();


                //Pero ha disminuido de 2.5
                if(velocidad < 2.5){
					if (!temporizadorActivo)
					{   //Entonces activamos el temporizador y comprobamos durante x min su velocidad
                        velocidadInferior = true; //La velocidad es inferior
						temporizador.Enabled = true;
						temporizadorActivo = true;
					}
                }

                //Mientras el temporizador esta activo
                if(temporizador.Enabled){
                    //Y la velocidad vuelve a aumentar
                    if(velocidad > 2.5){ 
                        //La velocidadInferior la ponemos a false
                        velocidadInferior = false;
                        temporizadorActivo = false;
                        temporizador.Enabled = false;
                    }

                    //Si la velocidad esta comprendida entre 0 y 0.2 y la velocidadInferior es igual a true, entonces nos guardamos las coordenadas por si acaso ha estacionado
                    if(velocidad > 0 && velocidad < 0.4 && velocidadInferior){
						latitudAparcado = latitud;
						longitudAparcado = longitud;
                        Console.WriteLine("Guardada la latitud: " + latitudAparcado + " y la longitud: " + longitudAparcado);
                    }
					Console.WriteLine("Temporizador esta activo: " + temporizador.Enabled);
                }

                //Si el temporizador ha terminado y la velocidadInferior es true, esto quiere decir que durante x minutos no ha acelerado, por lo que posiblemente haya estacionado
                if(!temporizador.Enabled && velocidadInferior){
					//Nos conectamos a la api, aunque anteriormente podriamos habernos conectado para probar si ha estado, pero cuando ha disminuido a 0 nos hemos guardado las coordenadas

                    id_parking = await ComprobarParking(latitudAparcado, longitudAparcado);

                    if (id_parking != -1 && !hasAparcado)
					{

                        Utils.UserCredentials.saveLatitudAparcado(latitudAparcado.ToString(), this);
                        Utils.UserCredentials.saveLongitudAparcado(longitudAparcado.ToString(), this);

						Console.WriteLine("HAS APARCADO EN EL PARKING :" + id_parking);

                        SendToMain(latitudAparcado, longitudAparcado);

                        hasAparcado = true;

                        bool sumarSitio = await _parkingService.SumarSitio(id_parking, Constants.vehiculo.tipo_vehiculo);

                        if(sumarSitio) {
                            Console.WriteLine("SUMADO SITIO: " + sumarSitio);
                            bool sumarPuntos = await _usuarioService.SumarPuntos("10", Constants.usuario.id_usuario.ToString());

                            if(sumarPuntos)
                            {
                                Constants.usuario.puntos_usuario += 10; //Sumo puntos a la constante tambien
                                Console.WriteLine("SUMADO PUNTOS: " + sumarPuntos);
                                hasAparcado = false;
                            }
                        }


                        //Guardamos como constante donde ha aparcado tendra que ser en preferencias de Android
                        MostrarAlertaAparcado();

                    }else if(id_parking == -1){
                        Console.WriteLine("NO HAS APARCADO EN NINGUN PARKING DE APARCAME :" + id_parking);

                        Utils.UserCredentials.saveLatitudAparcado(latitudAparcado.ToString(), this);
                        Utils.UserCredentials.saveLongitudAparcado(longitudAparcado.ToString(), this);

                        //MostrarAlertaAparcado(); solo en un parking de aparcame sale el mensaje
                    }

					velocidadSuperior = false; //Reiniciamos la velociadSuperior
					velocidadInferior = false; //Reiniciamos la velociadInferior
					temporizadorActivo = false; //Reiniciamos la variable temporizador
                }

            }else{
                Console.WriteLine("No has tenido una velocidad superior a 2.5");
                //Console.WriteLine("Has aparcado en: " + Utils.Constants.latitudAparcado + " " + Utils.Constants.longitudAparcado);
            }

        }

        //Con este metodo nos conectamos a la api para saber si esta en algun parking
        public async Task<int> ComprobarParking(double latitud, double longitud){

			int id = await _parkingService.ComprobarParking(latitude, longitude, "03690");

            return id;
        }


        //Mostrar alerta aparcado
        private void MostrarAlertaAparcado()
        {
            if (!Utils.UserCredentials.getAlertaAparcado(this))
            {
                var aparcado = new Intent(this, typeof(AparcadoActivity));
                aparcado.PutExtra("id", id_parking);
                StartActivity(aparcado);
                Utils.UserCredentials.saveAlertaAparcado(true, this);

                //Enviamos al main la ubicacion de aparcado
                SendToMain(latitudAparcado, longitudAparcado);


            }
        }

        //Mostrar alerta conduciendo
        private void MostrarAlertaConduciendo()
        {
            if (!Utils.UserCredentials.getAlertaConduciendo(this))
            {
                var conduciendo = new Intent(this, typeof(ConduciendoActivity));
                StartActivity(conduciendo);
                Utils.UserCredentials.saveAlertaConduciendo(true, this);
            }
        }

        private class TimerTaskToGetLocation : TimerTask
        {

            public EventHandler _eventHandler;
            public override void Run()
            {
                mHandler.Post(new Runnable(run){
                    
                });
            }

            void run()
            {
                _eventHandler.Invoke(this, null);
            }
        }



        //Con esto enviamos al mainActivity el id del parking
        private void SendToMain(double lat, double lng)
		{
            Console.WriteLine("Enviando...");
            Console.WriteLine(lat);
            Console.WriteLine(lng);

            intent.PutExtra("latitude", lat.ToString());
            intent.PutExtra("longitude", lng.ToString());
           
            SendBroadcast(intent);
		}



        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            
        }

        //Metodo que comprueba la ubicacion
        public void fn_getlocation()
        {
            locationManager = (LocationManager)ApplicationContext.GetSystemService(LocationService);
            isGPSEnable = locationManager.IsProviderEnabled(LocationManager.GpsProvider);
            isNetworkEnable = locationManager.IsProviderEnabled(LocationManager.NetworkProvider);

            if (!isGPSEnable && !isNetworkEnable)
            {

            }
            else
            {
                if (isGPSEnable)
                {
                    Console.WriteLine("Estas conectado a la red de GPS");
                    location = null;
                    locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 1000, 0, this);
                    if (locationManager != null)
                    {
                        location = locationManager.GetLastKnownLocation(LocationManager.GpsProvider);
                        if (location != null)
                        {
                            Console.WriteLine("latitude " + location.Latitude + "");
                            Console.WriteLine("longitude " + location.Longitude + "");
                            Console.WriteLine("velocity " + location.Speed + "");

                            latitude = location.Latitude;
                            longitude = location.Longitude;

                            float velocidad = 3.5f; //Para pruebas
                            ComprobarVelocidad(location.Speed, location.Latitude, location.Longitude);
                        }
                        else
                        {
                            Console.WriteLine("Location es null");
                        }
                    }
                }
                else if (isNetworkEnable)
                {
                    Console.WriteLine("Estas conectado a la red de internet");
                    location = null;
                    locationManager.RequestLocationUpdates(LocationManager.NetworkProvider, 1000, 0, this);
                    if (locationManager != null)
                    {
                        location = locationManager.GetLastKnownLocation(LocationManager.NetworkProvider);
                        if (location != null)
                        {
                            Console.WriteLine("latitude " + location.Latitude + "");
                            Console.WriteLine("longitude " + location.Longitude + "");
                            Console.WriteLine("velocity " + location.Speed + "");

                            latitude = location.Latitude;
                            longitude = location.Longitude;

                            float velocidad = 3.5f; //Para pruebas
                            ComprobarVelocidad(location.Speed, location.Latitude, location.Longitude);


                        }
                        else
                        {
                            Console.WriteLine("Location es null");
                        }
                    }
                }
            }

        }

    }
}
