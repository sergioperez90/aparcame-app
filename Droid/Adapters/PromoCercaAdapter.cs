using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace aparcame.Droid.Adapters
{
	public class PromoCercaAdapter : BaseAdapter
	{
		private Context context;
		private List<string> items; //Esto habra que cambiarlo en vez de <string> de tipo <Ofertas>

		public PromoCercaAdapter(Context c, List<string> items)
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

		//Aqui tendra que ser de tipo Ofertas
		public string getItem(int position)
		{
			return items[position];
		}

		public override View GetView(int position, View view, ViewGroup viewGroup)
		{
			LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);

			view = inflater.Inflate(Resource.Layout.item_cerca, viewGroup, false);

			//Esto habra que cambiarlo por el tipo Ofertas y luego sustituir los textivew por los items
			string item = getItem(position);

			TextView titulo = (TextView)view.FindViewById(Resource.Id.titulo_cerca);
			TextView lugar = (TextView)view.FindViewById(Resource.Id.lugar_cerca);
			TextView poblacion = (TextView)view.FindViewById(Resource.Id.poblacion_cerca);
			TextView puntos = (TextView)view.FindViewById(Resource.Id.puntos_cerca);
			ImageView imagen = (ImageView)view.FindViewById(Resource.Id.imagen_cerca);
			TextView distancia = (TextView)view.FindViewById(Resource.Id.distancia_cerca);
         
			//Esto es dinamico habra que sustituir
			if (position == 0)
			{
				titulo.Text = "Pizza Gratis en 2x1";
				lugar.Text = "Il Timone Express";
				poblacion.Text = "Altea (Alicante) 03590";
				puntos.Text = "200pt";
				distancia.Text = "22km";
                imagen.SetImageResource(Resource.Drawable.pizza);

			}
			else if (position == 1)
			{
				titulo.Text = "Copa Gratis";
				lugar.Text = "You Chic";
				poblacion.Text = "Altea (Alicante) 03590";
				puntos.Text = "1000pt";
				distancia.Text = "20km";
                imagen.SetImageResource(Resource.Drawable.copa);
			}

			return view;
		}
	}
}
