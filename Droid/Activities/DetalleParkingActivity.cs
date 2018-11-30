
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using aparcame.Droid.Fragments.Detalle;
using aparcame.Models;
using aparcame.Services;
using Java.Lang;

namespace aparcame.Droid.Activities
{
	[Activity(Label = "DetalleParkingActivity", Theme = "@style/AppTheme")]
	public class DetalleParkingActivity : BaseActivity
    {
		private ViewPager mViewPager;
        private TextView totalesActuales, plazasTotales, adaptadosActuales, adaptadosTotales, comunesActuales, comunesTotales, electricosActuales, electricosTotales, tituloParking;
        private string id_parking;
        private IParkingService _parkingService;
        private Parking parking;

		protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.detalle_parking_activity);

			base.initToolBar();

            //Recibo el id del parking
            id_parking = Intent.GetStringExtra("id");

            //Inicializamos las vistas
            totalesActuales = (TextView)FindViewById(Resource.Id.totalesActuales);
            plazasTotales = (TextView)FindViewById(Resource.Id.plazasTotales);
            adaptadosActuales = (TextView)FindViewById(Resource.Id.adaptadosActuales);
            adaptadosTotales = (TextView)FindViewById(Resource.Id.adaptadosTotales);
            comunesActuales = (TextView)FindViewById(Resource.Id.comunesActuales);
            comunesTotales = (TextView)FindViewById(Resource.Id.comunesTotales);
            electricosActuales = (TextView)FindViewById(Resource.Id.electricosActuales);
            electricosTotales = (TextView)FindViewById(Resource.Id.electricosTotales);
            tituloParking = (TextView)FindViewById(Resource.Id.tituloParking);


            //Inicializamos variables
            _parkingService = new ParkingService();
            parking = new Parking();

            //Para las tabs
            mViewPager = (ViewPager)FindViewById(Resource.Id.pager_detalle);
            TabLayout tabs = (TabLayout)FindViewById(Resource.Id.tabs_detalle);
			tabs.SetupWithViewPager(mViewPager);
			mViewPager.SetCurrentItem(0, true);

            //Cargar detalle del parking
            CargarDetalleParking();

        }

        /// <summary>
        /// Metodo para cargar el detalle del parking
        /// </summary>
        private async void CargarDetalleParking()
        {
            if(id_parking != null)
            {
                ProgressDialog pr = new Android.App.ProgressDialog(this);
                pr.SetMessage("Un momento...");
                pr.SetCancelable(false);
                pr.Show();

                parking = await _parkingService.DameParkingPorId(id_parking);

                if(parking != null)
                {
                    totalesActuales.Text = (parking.cant_usuario_normal_parking + parking.cant_usuario_minus_parking + parking.cant_usuario_energ_parking).ToString();
                    plazasTotales.Text = (parking.normal_total_parking + parking.minus_total_parking + parking.cant_usuario_energ_parking).ToString();
                    adaptadosActuales.Text = parking.cant_usuario_minus_parking.ToString();
                    adaptadosTotales.Text = parking.minus_total_parking.ToString();
                    comunesActuales.Text = parking.cant_usuario_normal_parking.ToString();
                    comunesTotales.Text = parking.normal_total_parking.ToString();
                    electricosTotales.Text = parking.energ_total_parking.ToString();
                    electricosActuales.Text = parking.cant_usuario_energ_parking.ToString();
                    tituloParking.Text = parking.nombre_parking;
        
                }

                //Llamamos a los fragments desde aqui
                setupViewPager(mViewPager);

                pr.Dismiss();
            }
        }      

		/// <summary>
        /// Metodo para añadir los fragments
        /// </summary>
        /// <param name="viewPager">View pager.</param>
        private void setupViewPager(ViewPager viewPager)
		{
            SectionsPagerAdapter adapter = new SectionsPagerAdapter(SupportFragmentManager);
            adapter.addFragment(AcercaParkingFragment.newInstance(1, parking), GetString(Resource.String.title_section5));
			adapter.addFragment(ActividadFragment.newInstance(2), GetString(Resource.String.title_section4));

			viewPager.Adapter = (adapter);
		}

		/// <summary>
        /// Clase para adaptar los fragments
        /// </summary>
		public class SectionsPagerAdapter : FragmentPagerAdapter
		{
			private List<Android.Support.V4.App.Fragment> mFragments = new List<Android.Support.V4.App.Fragment>();
			private List<string> mFragmentTitles = new List<string>();

			public SectionsPagerAdapter(Android.Support.V4.App.FragmentManager fm) : base(fm)
			{
				// base(fm);
			}

			public override Android.Support.V4.App.Fragment GetItem(int position)
			{
				return mFragments[position];
			}

			public override int Count
			{
				get
				{
					return mFragments.Count;
				}
			}

			public void addFragment(Android.Support.V4.App.Fragment fragment, string title)
			{
				mFragments.Add(fragment);
				mFragmentTitles.Add(title);
			}

			public override ICharSequence GetPageTitleFormatted(int position)
			{
				string a = mFragmentTitles[position];
				Java.Lang.String javaA = new Java.Lang.String(a);

				return javaA;
			}
		}
    }
}
