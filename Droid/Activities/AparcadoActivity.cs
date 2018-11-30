
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using aparcame.Models;
using aparcame.Services;

namespace aparcame.Droid.Activities
{
    [Activity(Label = "AparcadoActivity", Theme = "@style/AppTheme")]
    public class AparcadoActivity : Activity
    {
        Geocoder geocoder;
        private IParkingService _parkingService;
        private Parking parking;
        private int id_parking;
        TextView direccionParking, nombreParking;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_aparcado);


            ImageView cerrarFiltro = (ImageView)FindViewById(Resource.Id.cerrar_filtro);
            Button entendido = (Button)FindViewById(Resource.Id.btnEntendido);
            nombreParking = (TextView)FindViewById(Resource.Id.parkingAparcado);
            direccionParking = (TextView)FindViewById(Resource.Id.parkingDireccion);

            cerrarFiltro.Click += delegate
            {
                Finish();
            };

            entendido.Click += delegate
            {
                Finish();
            };

            //Recibo el id del parking
            id_parking = Intent.GetIntExtra("id", -1);

            if (id_parking != -1)
            {
                DameParkingPorId();
            }
            else
            {
                nombreParking.Text = "Ningún parking de Apárcame";
                direccionParking.Text = "Pero hemos guardado su ubicación";
            }
        }

        /// <summary>
        /// Metodo q obtiene el parking por Id
        /// </summary>
        private async void DameParkingPorId()
        {
            ProgressDialog pr = new Android.App.ProgressDialog(this);
            pr.SetMessage("Un momento...");
            pr.SetCancelable(false);
            pr.Show();
            _parkingService = new ParkingService();
            parking = new Parking();

            parking = await _parkingService.DameParkingPorId(id_parking.ToString());

            if(parking != null)
            {
                nombreParking.Text = parking.nombre_parking;
                //CargarDireccionParking();
            }

            pr.Dismiss();
        }

        /// <summary>
        /// Cargar la direccion del parking
        /// </summary>
        private void CargarDireccionParking()
        {
            //Sacamos la direccion
            IList<Address> addresses = new List<Address>();
            try
            {
                addresses = geocoder.GetFromLocation(parking.latitud_parking, parking.longitud_parking, 1);
                string direccion = addresses[0].GetAddressLine(1);

                direccionParking.Text = direccion;
            }
            catch (IOException e)
            {

            }
        }      
    }
}
