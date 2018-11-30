
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
    public class MisDatosFragment : Fragment
    {
		private View rootView;
		private const string ARG_SECTION_NUMBER = "section_number";

		public static MisDatosFragment newInstance(int sectionNumber)
		{
			MisDatosFragment fragment = new MisDatosFragment();
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
                rootView = inflater.Inflate(Resource.Layout.misdatos_fragment, container, false);
                EditText nombre = (EditText)rootView.FindViewById(Resource.Id.nombre_datos);
                EditText apellidos = (EditText)rootView.FindViewById(Resource.Id.apellidos_datos);
                EditText email = (EditText)rootView.FindViewById(Resource.Id.email_datos);

                if (Constants.usuario.nombre_usuario != null) nombre.Text = Constants.usuario.nombre_usuario;
                if (Constants.usuario.apellidos_usuario != null) apellidos.Text = Constants.usuario.apellidos_usuario;
                if (Constants.usuario.email_usuario != null) email.Text = Constants.usuario.email_usuario;
			}

			return rootView;
		}
    }
}
