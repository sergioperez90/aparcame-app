
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
using aparcame.Droid.Activities;
using aparcame.Droid.Adapters;

namespace aparcame.Droid.Fragments.Promociones
{
    public class CercaFragment : Fragment
    {
		private View rootView;
		private const string ARG_SECTION_NUMBER = "section_number";
		private PromoCercaAdapter cercaAdapter;
		private ListView listCerca;

		public static CercaFragment newInstance(int sectionNumber)
		{
			CercaFragment fragment = new CercaFragment();
			Bundle args = new Bundle();
			args.PutInt(ARG_SECTION_NUMBER, sectionNumber);
			fragment.Arguments = (args);
			return fragment;
		}


		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
           
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			if (rootView == null)
			{
                rootView = inflater.Inflate(Resource.Layout.cerca_fragment, container, false);

                listCerca = (ListView)rootView.FindViewById(Resource.Id.lista_cerca);

				//Esto despues habra que cargar la lista de Destacadas real
				List<string> cerca = new List<string>();
				cerca.Add("Sin contenido");
				cerca.Add("Sin contenido");

				cercaAdapter = new PromoCercaAdapter(this.Activity, cerca);
				listCerca.Adapter = cercaAdapter;

				//Al hacer click en un item
				listCerca.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
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
