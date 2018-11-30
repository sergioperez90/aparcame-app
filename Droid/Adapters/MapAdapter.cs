using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using aparcame.Models;
using aparcame.Services;
using aparcame.Droid.Utils;
using Android.Content.Res;
using static Android.Gms.Maps.GoogleMap;
using aparcame.Droid.Activities;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using System.Linq;
using Android.Graphics.Drawables;

namespace aparcame.Droid.Adapters
{
    public class MapAdapter
    {
		private GoogleMap mMap;
		private Context context;
		List<Parking> listaParkings;
		private static IParkingService _parkingService;
        LatLng tuPos;
        public EventHandler <string> clickMarcador;
		string _locationProvider;


        public MapAdapter(GoogleMap map, Context context)
		{
			this.mMap = map;
			this.context = context;
			listaParkings = new List<Parking>();
			_parkingService = new ParkingService();

            if(UserCredentials.getLatitudUbicacion(context) != null && UserCredentials.getLongitudUbicacion(context) != null)
            {
                double lat = Convert.ToDouble(UserCredentials.getLatitudUbicacion(context));
                double lng = Convert.ToDouble(UserCredentials.getLongitudUbicacion(context));

                tuPos = new LatLng(lat, lng); 
            }
            else
            {
                tuPos = new LatLng(38.385689, -0.514634); 
            }
			


			mMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(tuPos, 15));

		}
     
        //TODO Habra que cargar comercios y gasolineras

        /// <summary>
        /// Metodo que carga los parkings
        /// </summary>
        public async void cargarParkings()
        {
            ProgressDialog pr = new Android.App.ProgressDialog(context);
			pr.SetMessage("Cargando parkings...");
			pr.SetCancelable(false);
            pr.Show();
            listaParkings = await _parkingService.DameParkings();

            cargarMarcadores();

            pr.Dismiss();

		}

        /// <summary>
        /// Metodo para cargar los marcadores
        /// </summary>
        public void cargarMarcadores()
        {         
			foreach (var parkings in listaParkings)
			{
                //Cantidad de usuarios totales por defecto mostraremos esto
                int usuariosTotales = parkings.cant_usuario_normal_parking + parkings.cant_usuario_minus_parking + parkings.cant_usuario_energ_parking;
                int plazasTotales = parkings.normal_total_parking + parkings.minus_total_parking + parkings.energ_total_parking;

    			//Creamos un canvas para modificar la imagen y añadir texto encima
    			Bitmap.Config conf = Bitmap.Config.Argb8888;
                Bitmap bmp = Bitmap.CreateBitmap((int)context.Resources.GetDimension(Resource.Dimension.ancho_canvas), (int)context.Resources.GetDimension(Resource.Dimension.alto_canvas), conf);
    			Canvas canvas1 = new Canvas(bmp);
    			Paint numDisp = new Paint();
                numDisp.TextSize = (int) context.Resources.GetDimension(Resource.Dimension.tamanyo_numero_disp);
    			numDisp.Color = Color.White;
    			numDisp.SetTypeface(Typeface.Create(Typeface.Default, TypefaceStyle.Bold));
    			canvas1.DrawBitmap(BitmapFactory.DecodeResource(this.context.Resources,
                                                                ColorMarker.calcular(plazasTotales, usuariosTotales)), 0, 0, numDisp);

                if(usuariosTotales > 99){
                    canvas1.DrawText(usuariosTotales.ToString(), (int)context.Resources.GetDimension(Resource.Dimension.posXMayor_num), (int)context.Resources.GetDimension(Resource.Dimension.posY_num), numDisp);
                }else if(usuariosTotales > 9 && usuariosTotales < 100){
                    canvas1.DrawText(usuariosTotales.ToString(), (int)context.Resources.GetDimension(Resource.Dimension.posX_num), (int)context.Resources.GetDimension(Resource.Dimension.posY_num), numDisp);
                }else if(usuariosTotales >= 0 && usuariosTotales < 10){
                    canvas1.DrawText(usuariosTotales.ToString(), (int)context.Resources.GetDimension(Resource.Dimension.posXmenor_num), (int)context.Resources.GetDimension(Resource.Dimension.posY_num), numDisp);

    			}

                //Añadimos el marcador
                mMap.AddMarker(new MarkerOptions()
                           .SetTitle(parkings.id_parking.ToString())
                           .SetPosition(new LatLng(parkings.latitud_parking, parkings.longitud_parking))
                           .SetIcon(BitmapDescriptorFactory.FromBitmap(bmp)));
                           
    		    }    

            //Cuando haces click en un marcador
            mMap.MarkerClick += (object sender, MarkerClickEventArgs e) => {

                //Si es null es porque es el icono de aparcado
                if(e.Marker.Title != null)
                {
                    clickMarcador.Invoke(null, e.Marker.Title);
                }           
			};
            
            //Comprobamos si esta aparcado
            comprobarAparcado();
        }

        /// <summary>
		/// Metodo que comprueba si ya ha aparcado el coche
        /// </summary>
        public void comprobarAparcado()
        {
            if (UserCredentials.getLatitudAparcado(context) != null && UserCredentials.getLongitudAparcado(context) != null)
            {
                LatLng latLongAparcado = new LatLng(Convert.ToDouble(UserCredentials.getLatitudAparcado(context)), Convert.ToDouble(UserCredentials.getLongitudAparcado(context)));
                BitmapDescriptor pinAparcado = BitmapDescriptorFactory.FromResource(Resource.Drawable.pin_aparcado);
                            
                mMap.AddMarker(new MarkerOptions()
                               .InvokeZIndex(10000)
                               .SetTitle(null)
                               .SetPosition(latLongAparcado)
                               .SetIcon(pinAparcado));
            }

        }

        /// <summary>
        /// Metodo para actualizar los marcadores
        /// </summary>
        public async void ActualizarMarcadores()
        {
            listaParkings = await _parkingService.DameParkings();

            mMap.Clear();

            cargarMarcadores();
        }
    }
}
