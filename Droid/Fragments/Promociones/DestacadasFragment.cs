
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
using aparcame.Droid.Adapters;

namespace aparcame.Droid.Fragments.Promociones
{
    public class DestacadasFragment : Fragment
    {
		private View rootView;
		private const string ARG_SECTION_NUMBER = "section_number";
		private PromoDestacadasAdapter destacadasAdapter;
		private ListView listDestacadas;

		public static DestacadasFragment newInstance(int sectionNumber)
		{
			DestacadasFragment fragment = new DestacadasFragment();
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
                rootView = inflater.Inflate(Resource.Layout.destacadas_fragment, container, false);

                listDestacadas = (ListView)rootView.FindViewById(Resource.Id.lista_destacadas);

				//Esto despues habra que cargar la lista de Destacadas real
				List<string> destacadas = new List<string>();
				destacadas.Add("Sin contenido");
				destacadas.Add("Sin contenido");
				destacadas.Add("Sin contenido");

				destacadasAdapter = new PromoDestacadasAdapter(this.Activity, destacadas);
				listDestacadas.Adapter = destacadasAdapter;

				//Al hacer click en un item
				listDestacadas.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
				{
					//Le tendremos que pasar la informacion de la oferta donde se ha hecho click               
				};            
			}

			return rootView;
		}
    }
}
