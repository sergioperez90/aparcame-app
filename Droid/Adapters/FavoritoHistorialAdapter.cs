using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;
namespace aparcame.Droid.Adapters
{
	public class FavoritoHistorialAdapter : BaseAdapter
	{
		private Context context;
		private List<string> items; //Esto habra que cambiarlo en vez de <string> de tipo <Favorito>


		public FavoritoHistorialAdapter(Context c, List<string> items)
		{
			this.context = c;
			this.items = items;
		}

		public override int Count
		{
			get
			{
				return items.Count;
			}
		}
		public override Java.Lang.Object GetItem(int position)
		{
			return null;
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		//Aqui tendra que ser de tipo Favorito
		public string getItem(int position)
		{
			return items[position];
		}


		public override View GetView(int position, View view, ViewGroup viewGroup)
		{
			LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);

            view = inflater.Inflate(Resource.Layout.favorito_item, viewGroup, false);
            
			//Esto habra que cambiarlo por el tipo Favorito y luego sustituir los textivew por los items
			string item = getItem(position);

            TextView titulo = (TextView)view.FindViewById(Resource.Id.titulo_favorito);
            TextView lugar = (TextView)view.FindViewById(Resource.Id.lugar_favorito);
            TextView poblacion = (TextView)view.FindViewById(Resource.Id.poblacion_favorito);
            ImageView imagen = (ImageView)view.FindViewById(Resource.Id.imagen_favorito);

   			//Esto es dinamico
			if (position == 0)
			{
				titulo.Text = "Copa Gratis";
				lugar.Text = "You Chic";
				poblacion.Text = "Altea (Alicante) 03590";
                imagen.SetImageResource(Resource.Drawable.copa);


			}
			else if (position == 1)
			{
				titulo.Text = "Gasolina gratis";
				lugar.Text = "Galp";
				poblacion.Text = "Altea (Alicante) 03590";
                imagen.SetImageResource(Resource.Drawable.gasolinera);
			}

			return view;
		}
	}
}
