
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using aparcame.Droid.Adapters;
using aparcame.Droid.Activities;

namespace aparcame.Droid.Fragments.DetalleOferta
{
    public class MasOfertasFragment : Fragment
    {
		private const string ARG_SECTION_NUMBER = "section_number";
		private View rootView;
        private MasOfertasAdapter masOfertasAdapter;
        private ListView listMasOfertas;


		public static MasOfertasFragment newInstance(int sectionNumber)
		{
			MasOfertasFragment fragment = new MasOfertasFragment();
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
                rootView = inflater.Inflate(Resource.Layout.mas_ofertas_fragment, container, false);

                listMasOfertas = (ListView)rootView.FindViewById(Resource.Id.lista_mas_ofertas_detalle);

				//Esto despues habra que cargar la lista de Ofertas real;
				List<string> ofertas = new List<string>();
				ofertas.Add("Sin contenido");
				ofertas.Add("Sin contenido");
				ofertas.Add("Sin contenido");

                masOfertasAdapter = new MasOfertasAdapter(this.Activity, ofertas);
				listMasOfertas.Adapter = masOfertasAdapter;

				//Al hacer click en un item
				listMasOfertas.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
				{
					//Le tendremos que pasar la informacion de la oferta donde se ha hecho click
					Intent i = new Intent(Activity, typeof(DetalleOfertaActivity));
					StartActivity(i);
				};            
			}

			return rootView;
		}
    }
}
