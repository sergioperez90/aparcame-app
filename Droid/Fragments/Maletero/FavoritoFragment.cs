
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using aparcame.Droid.Activities;
using aparcame.Droid.Adapters;

namespace aparcame.Droid.Fragments.Maletero
{
    public class FavoritoFragment : Android.Support.V4.App.Fragment
    {
        private View rootView;
		private const string ARG_SECTION_NUMBER = "section_number";
		private FavoritoHistorialAdapter favoritoAdapter;
		private ListView listFavorito;

		public static FavoritoFragment newInstance(int sectionNumber)
		{
			FavoritoFragment fragment = new FavoritoFragment();
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
                rootView = inflater.Inflate(Resource.Layout.favoritos_fragment, container, false);

                listFavorito = (ListView)rootView.FindViewById(Resource.Id.listview_favorito);

				//Esto despues habra que cargar la lista de Favoritos real;
				List<string> historial = new List<string>();
				historial.Add("Sin contenido");
				historial.Add("Sin contenido");

                favoritoAdapter = new FavoritoHistorialAdapter(this.Activity, historial);
				listFavorito.Adapter = favoritoAdapter;

				//Al hacer click en un item
				listFavorito.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
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
