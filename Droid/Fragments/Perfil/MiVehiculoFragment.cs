
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace aparcame.Droid.Fragments.Perfil
{
    public class MiVehiculoFragment : Fragment
    {
		private View rootView;
		private const string ARG_SECTION_NUMBER = "section_number";

		public static MiVehiculoFragment newInstance(int sectionNumber)
		{
			MiVehiculoFragment fragment = new MiVehiculoFragment();
			Bundle args = new Bundle();
			args.PutInt(ARG_SECTION_NUMBER, sectionNumber);
			fragment.Arguments = (args);
			return fragment;
		}

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{

			if (rootView == null)
			{
				rootView = inflater.Inflate(Resource.Layout.mivehiculo_fragment, container, false);
                EditText tipo = (EditText)rootView.FindViewById(Resource.Id.tipoVehiculo_datos);
                EditText marca = (EditText)rootView.FindViewById(Resource.Id.marca_datos);
                EditText modelo = (EditText)rootView.FindViewById(Resource.Id.modelo_datos);
                EditText combustible = (EditText)rootView.FindViewById(Resource.Id.combustible_datos);

                if (Constants.vehiculo.tipo_vehiculo != -1){

                    if(Constants.vehiculo.tipo_vehiculo == 1){
                        tipo.Text = "Común";
                    }
                    else if (Constants.vehiculo.tipo_vehiculo == 2)
                    {
                        tipo.Text = "Adaptado";
                    }
                    else if (Constants.vehiculo.tipo_vehiculo == 3)
                    {
                        tipo.Text = "Eléctrico";
                    }

                }
                if (Constants.vehiculo.marca_vehiculo != null) marca.Text = Constants.vehiculo.marca_vehiculo;
                if (Constants.vehiculo.modelo_vehiculo != null) modelo.Text = Constants.vehiculo.modelo_vehiculo;
                if (Constants.vehiculo.combustible_vehiculo != null) combustible.Text = Constants.vehiculo.combustible_vehiculo;
			}

			return rootView;
		}
    }
}
