using Android.App;
using Android.Widget;
using Android.OS;
using Android.Gms.Maps;
using System;
using aparcame.Droid.Adapters;
using Android.Gms.Maps.Model;
using Android.Support.V4.App;
using System.Collections.Generic;
using aparcame.Models;
using aparcame.Services;
using Android.Content;
using aparcame.Droid.Services;
using Android.Support.V4.Content;
using Android.Preferences;
using Android.Views;
using Android.Support.V4.Widget;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using Java.Util;
using Java.Lang;
using aparcame.Droid.Activities;
using Android.Locations;
using aparcame.Droid.Utils;

namespace aparcame.Droid
{
    [Activity(Label = "Apárcame", Icon = "@mipmap/icon", Theme = "@style/AppTheme")]
    public class MainActivity : FragmentActivity, IOnMapReadyCallback
    {
        DrawerLayout drawerLayout;
        List<LinearLayout> anuncios;
        LinearLayout anuncio;
		NavigationView navigationView;
		private GoogleMap mMap;
        private MapAdapter mapAdapter;
        List<Parking> listaParkings;
        ViewPager viewPagerAnuncio;
        private static IParkingService _parkingService;
		private static Handler mHandler = new Handler();
        private AnuncioFragmentAdapter anuncioFragmentAdapter;
        private Java.Util.Timer mTimer = null;
        int cantidadAnuncios, posAnuncio = -1;
		Recibidor recibidor;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            //Iniciamos las vistas
			ImageView botonMenu = (ImageView)FindViewById(Resource.Id.boton_menu);
            ImageView filtroMapa = (ImageView)FindViewById(Resource.Id.boton_filtro);
            viewPagerAnuncio = FindViewById<ViewPager>(Resource.Id.viewPager);
			drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
			navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);


            //Iniciamos variables
            listaParkings = new List<Parking>();
            _parkingService = new ParkingService();
            recibidor = new Recibidor();
            UserCredentials.saveAlertaConduciendo(false, this);
            UserCredentials.saveAlertaAparcado(false, this);


            //Iniciamos metodos
            NavigationDrawer();
            //cargarAnuncios();


            //Cargamos el mapa
            MapFragment mapFrag = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
            mapFrag.GetMapAsync(this);
			

            //Eventos
            filtroMapa.Click += delegate {
                var detalleActivity = new Intent(this, typeof(FiltroMapaActivity));
				StartActivity(detalleActivity);
            };


			botonMenu.Click += delegate {
                drawerLayout.OpenDrawer(GravityCompat.Start);
            };


            recibidor.ValueChanged += (s, responseData) => //Recibo los parametros del servicio
            {
                string[] location;
                location = responseData.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);

                //Añadimos el marcador de aparcado

                if(location[0] != "0" && location[1] != "0")
                {
                    /*LatLng latLongAparcado = new LatLng(Convert.ToDouble(location[0]), Convert.ToDouble(location[1]));
                    BitmapDescriptor pinAparcado = BitmapDescriptorFactory.FromResource(Resource.Drawable.pin_aparcado);

                    mMap.AddMarker(new MarkerOptions()
                                   .InvokeZIndex(10000)
                                   .SetTitle(null)
                                   .SetPosition(latLongAparcado)
                                   .SetIcon(pinAparcado));*/

                    if (mapAdapter != null) mapAdapter.ActualizarMarcadores();
                }



            };

            //Con esto movemos los anuncios
            /*var _timer = new TimerTaskAnuncios();
            mTimer = new Java.Util.Timer();
            mTimer.Schedule(_timer, 0, 6000); //Cada 5 segundos cambiamos de pagina

            _timer._eventHandler += delegate
            {
                moverAnuncios();
            };*/


            //Cargamos los ids de los parkings
            listaParkings = await _parkingService.DameParkings();
            foreach (var lista in listaParkings)
            {
                Console.WriteLine("Id de parking: " + lista.id_parking);
            }


            if(UserCredentials.getPermiso(this))
            {
                StartService(new Intent(this, typeof(BackgroundService)));    
            }
        }
  
        /// <summary>
        /// Navegacion de menu lateral
        /// </summary>
        public void NavigationDrawer ()
        {
			View headerView = LayoutInflater.Inflate(Resource.Layout.drawer_header, navigationView, false);
			navigationView.AddHeaderView(headerView);

			ImageView botonMaletero = (ImageView)headerView.FindViewById(Resource.Id.miMaletero);
			botonMaletero.Click += delegate
			{
				Intent i = new Intent(this, typeof(MiMaleteroActivity));
				StartActivity(i);
			};

			ImageView botonPerfil = (ImageView)headerView.FindViewById(Resource.Id.miPerfil);
			botonPerfil.Click += delegate
			{
				Intent i = new Intent(this, typeof(PerfilActivity));
				StartActivity(i);
			};

            TextView emailUsuario = (TextView)headerView.FindViewById(Resource.Id.email_header);
            emailUsuario.Text = UserCredentials.getEmailUsuario(this);



		    int size = navigationView.Menu.Size();
			for (int i = 0; i < size; i++)
			{
                navigationView.Menu.GetItem(i).SetChecked(false);
			}

			//Menu Lateral
			navigationView.NavigationItemSelected += (sender, e) =>
			{
				//e.MenuItem.SetChecked(false);

				var position = e.MenuItem.ItemId;

				if (position == Resource.Id.mapa)
				{

				}

                if (position == Resource.Id.cerrar_sesion)
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    alert.SetTitle("Cerrar sesión");
                    alert.SetMessage("¿Estás seguro que quieres cerrar sesión?");
                    alert.SetPositiveButton("Cerrar sesión", (senderAlert, args) => {
                        Utils.UserCredentials.removeAll(this);
                        Intent i = new Intent(this, typeof(PreRegisterActivity));
                        StartActivity(i);
                        Finish();
                    });

                    alert.SetNegativeButton("Cancelar", (senderAlert, args) => {

                
                    });

                    Dialog dialog = alert.Create();
                    dialog.Show();            
                }

                //todo OCULTADO PRIMERA VERSION
                /*else if (position == Resource.Id.promociones)
				{
                    Intent i = new Intent(this, typeof(PromocionesActivity));
					StartActivity(i);
                }else if(position == Resource.Id.cupon)
                {
                    Intent i = new Intent(this, typeof(CanjearCuponActivity));
					StartActivity(i);
                    
                }*/

                drawerLayout.CloseDrawers();
			};
		}
	
		public void OnMapReady(GoogleMap googleMap)
        {

            mMap = googleMap;

            //Si has aceptado los permisos de localización
            if (UserCredentials.getPermiso(this))
            {
                mapAdapter = new MapAdapter(this.mMap, this);

                mapAdapter.cargarParkings();


                //Click en el marcador
                mapAdapter.clickMarcador += (object sender, string e) => {

                    Console.WriteLine("Pinchado en el ID del parking " + e);

                    var detalleActivity = new Intent(this, typeof(DetalleParkingActivity));
                    detalleActivity.PutExtra("id", e);
                    StartActivity(detalleActivity);
                };
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            RegisterReceiver(recibidor, new IntentFilter("com.aparcame.aparcame.BackgroundService"));
        }

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			if (item.ItemId == Resource.Id.mapa)
			{

			}

			drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
			return true;
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }  
  

        /// <summary>
        /// Metodo para cargar los anuncios
        /// </summary>
        public void cargarAnuncios(){
            anuncioFragmentAdapter = new AnuncioFragmentAdapter(SupportFragmentManager);
            anuncios = new List<LinearLayout>();
            for (int i = 0; i < 5; i++){ //Estoy creando 5 anuncios, habra que cambiarlo por los que haya cerca
                crearAnuncio();
            }

            viewPagerAnuncio.PageMargin = 10;
            viewPagerAnuncio.Adapter = anuncioFragmentAdapter;

            moverAnuncios();
        }

        /// <summary>
        /// Metodo para crear los anuncios
        /// </summary>
        public void crearAnuncio(){
			anuncioFragmentAdapter.addFragmentView((arg1, arg2, arg3) =>
			{
				// Inflating View to access Image View
				var view = arg1.Inflate(Resource.Layout.anuncio_home, arg2, false);
                anuncio = view.FindViewById<LinearLayout>(Resource.Id.anuncio);
                anuncios.Add(anuncio);

                anuncios[anuncios.Count -1].Click += delegate {

                    Intent i = new Intent(this, typeof(DetalleOfertaActivity));
					StartActivity(i);

                };
     
				return view;
			});
        }

        /// <summary>
        /// Metodo para mover los anuncios
        /// </summary>
        public void moverAnuncios(){
            if(posAnuncio > 5){
                posAnuncio = 0;
                viewPagerAnuncio.SetCurrentItem(0, true);
			}else{
                viewPagerAnuncio.SetCurrentItem(posAnuncio++, true);

            }

        }

        /// <summary>
        /// Clase para crear un temporizador y poder mover anuncios
        /// </summary>
		private class TimerTaskAnuncios : TimerTask
		{

			public EventHandler _eventHandler;
			public override void Run()
			{

				mHandler.Post(new Runnable(run)
				{

				});
			}

			void run()
			{
				_eventHandler.Invoke(this, null);
			}
		}
            
    }

    /// <summary>
    /// Broadcast receiver, datos recibidos del servicio en segundo plano
    /// </summary>
	[BroadcastReceiver(Enabled = true, Exported = false)]
	[IntentFilter(new[] { "com.aparcame.aparcame.BackgroundService" })]
	public class Recibidor : BroadcastReceiver
	{
		public event EventHandler<string> ValueChanged;

		public override void OnReceive(Context context, Intent intent)
		{
            string latitud = intent.GetStringExtra("latitude");
            string longitud = intent.GetStringExtra("longitude");
			var handler = ValueChanged;
			if (handler != null)
			{
				handler(this, latitud + " " + longitud);
			}
		}

	}


}

