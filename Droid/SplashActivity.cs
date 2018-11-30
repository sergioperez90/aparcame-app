using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Util;
using aparcame.Droid.Activities;
using aparcame.Droid.Utils;
using aparcame.Models;
using aparcame.Services;
using Java.Lang;
using Java.Util;

namespace aparcame.Droid
{
	[Activity(Theme = "@style/AppTheme.Splash", MainLauncher = true, NoHistory = true)]
	public class SplashActivity : AppCompatActivity, ILocationListener
	{
        private static int REQUEST_PERMISSIONS = 100;
        private IUsuarioService _usuarioService;

		public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
		{
			base.OnCreate(savedInstanceState, persistentState);

		}      
        
        protected override void OnResume()
        {
            base.OnResume();

            PermisoLocation();

            //Comprobar permisos aceptados
            if (!UserCredentials.getPrimeraVez(this))
            {
                if (!UserCredentials.getPermiso(this))
                {
                    MensajeErrorUbicacion();
                }
                else
                {
                    Geolocation();
                    ComprobarLogin();
                }
            }

            //Ya no es la primera vez
            UserCredentials.savePrimeraVez(false, this);
        }

        /// <summary>
		/// Comprobamos si el usuario esta logueado
        /// </summary>
        private async void ComprobarLogin()
        {
            _usuarioService = new UsuarioService();
            Usuario usuario = await _usuarioService.Login(UserCredentials.getEmailUsuario(this), UserCredentials.getPassUsuario(this));
           
            if (usuario == null)
            {
                UserCredentials.saveEmailUsuario(null, this);
                UserCredentials.savePassUsuario(null, this);
                UserCredentials.saveIdUsuario(null, this);
                UserCredentials.saveTokenJWT(null, this);

                StartActivity(new Intent(Application.Context, typeof(PreRegisterActivity)));
            }
            else
            {
                ComprobarVehiculo();
                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            }
        }

        /// <summary>
		/// Metodo que comprueba el vehiculo del usuario
        /// </summary>
        private async void ComprobarVehiculo()
        {
            Vehiculo vehiculo = await _usuarioService.DameVehiculoUsuario(Constants.usuario.id_usuario.ToString());

            if(vehiculo != null)
            {
                Constants.vehiculo = vehiculo;
            }

        }
      
        /// <summary>
		/// Metodo de permisos de localizacion
        /// </summary>
        private void PermisoLocation()
        {
            if ((ContextCompat.CheckSelfPermission(ApplicationContext, Android.Manifest.Permission.AccessFineLocation) != Android.Content.PM.Permission.Granted))
            {

                if ((ActivityCompat.ShouldShowRequestPermissionRationale(this, Android.Manifest.Permission.AccessFineLocation)))
                {


                }
                else
                {
                    ActivityCompat.RequestPermissions(this, new string[]{Android.Manifest.Permission.AccessFineLocation

                        },
                            REQUEST_PERMISSIONS);
                }
            }
            else
            {
                UserCredentials.savePermiso(true, this);
            }
        }

        /// <summary>
		/// Metodo de cuando has aceptado los permisos
        /// </summary>
        /// <param name="requestCode">Request code.</param>
        /// <param name="permissions">Permissions.</param>
        /// <param name="grantResults">Grant results.</param>
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
         
            if (grantResults.Length > 0 && grantResults[0] == Android.Content.PM.Permission.Granted)
            {
                Geolocation();
                UserCredentials.savePermiso(true, this);
            }
            else
            {
                //Si no has aceptado los permisos de ubicacion
                UserCredentials.savePermiso(false, this);
                MensajeErrorUbicacion();
            }

        }

        /// <summary>
		/// Mensaje de mostrar el error si no has aceptado los permisos
        /// </summary>
        public void MensajeErrorUbicacion()
        {
            Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
            alert.SetTitle("Ups!");
            alert.SetMessage("Debes aceptar los permisos de ubicación, ves a Ajustes de la aplicación, activa la localización y reinicia la aplicación");
            alert.SetCancelable(false);
            alert.SetPositiveButton("Aceptar", (senderAlert, args) => {
                Finish();

            });
            alert.Show();
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

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {

        }

        /// <summary>
        /// Metodo para la geolocalizacion
        /// </summary>
        public void Geolocation()
        {
            LocationManager locationManager = (LocationManager)ApplicationContext.GetSystemService(LocationService);
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

                    if(l != null)
                    {
                        UserCredentials.saveLatitudUbicacion(l.Latitude.ToString(), this);
                        UserCredentials.saveLongitudUbicacion(l.Longitude.ToString(), this);
                    }
                }
            }
        }

	}
}
