
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
    public class MiDireccionFragment : Fragment
    {
		private View rootView;
		private const string ARG_SECTION_NUMBER = "section_number";

		public static MiDireccionFragment newInstance(int sectionNumber)
		{
			MiDireccionFragment fragment = new MiDireccionFragment();
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
                rootView = inflater.Inflate(Resource.Layout.midireccion_fragment, container, false);
                EditText direccion = (EditText)rootView.FindViewById(Resource.Id.direccion_datos);
                EditText localidad = (EditText)rootView.FindViewById(Resource.Id.localidad_datos);
                EditText provincia = (EditText)rootView.FindViewById(Resource.Id.provincia_datos);
                EditText cp = (EditText)rootView.FindViewById(Resource.Id.cp_datos);

                if (Constants.usuario.direccion_usuario != null) direccion.Text = Constants.usuario.direccion_usuario;
                if (Constants.usuario.localidad_usuario != null) localidad.Text = Constants.usuario.localidad_usuario;
                if (Constants.usuario.provincia_usuario != null) provincia.Text = Constants.usuario.provincia_usuario;
                if (Constants.usuario.cp_usuario != null) cp.Text = Constants.usuario.cp_usuario;
			}

			return rootView;
		}
    }
}
