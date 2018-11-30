using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace aparcame.Droid.Adapters
{
    public class PromoDestacadasAdapter : BaseAdapter
    {
		private Context context;
		private List<string> items; //Esto habra que cambiarlo en vez de <string> de tipo <Ofertas>

		public PromoDestacadasAdapter(Context c, List<string> items)
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

            view = inflater.Inflate(Resource.Layout.item_destacado, viewGroup, false);

			//Esto habra que cambiarlo por el tipo Ofertas y luego sustituir los textivew por los items
			string item = getItem(position);

			TextView titulo = (TextView)view.FindViewById(Resource.Id.titulo_destacado);
			TextView lugar = (TextView)view.FindViewById(Resource.Id.lugar_destacado);
			TextView poblacion = (TextView)view.FindViewById(Resource.Id.poblacion_destacado);
			TextView puntos = (TextView)view.FindViewById(Resource.Id.puntos_destacado);
            ImageView imagen = (ImageView)view.FindViewById(Resource.Id.imagen_destacado);
			ImageView mascara = (ImageView)view.FindViewById(Resource.Id.mascara_destacado);
            LinearLayout contenidoTexto = (LinearLayout)view.FindViewById(Resource.Id.linear_destacado);
         
			//Esto es dinamico habra que sustituir
			if (position == 0)
			{
				titulo.Text = "Pizza Gratis en 2x1";
				lugar.Text = "Il Timone Express";
				poblacion.Text = "Altea (Alicante) 03590";
				puntos.Text = "200pt";
                imagen.SetImageResource(Resource.Drawable.destacado_pizza);
                mascara.SetImageResource(Resource.Drawable.mascara_destacado_rojo);
                contenidoTexto.SetBackgroundColor(Color.ParseColor("#b21340"));

			}
			else if (position == 1)
			{
				titulo.Text = "Copa Gratis";
				lugar.Text = "You Chic";
				poblacion.Text = "Altea (Alicante) 03590";
				puntos.Text = "1000pt";
                imagen.SetImageResource(Resource.Drawable.destacado_copa);
                mascara.SetImageResource(Resource.Drawable.mascara_destacado_verde);
                contenidoTexto.SetBackgroundColor(Color.ParseColor("#13b265"));
			}
			else if (position == 2)
			{
				titulo.Text = "Gasolina gratis";
				lugar.Text = "Galp";
				poblacion.Text = "Altea (Alicante) 03590";
				puntos.Text = "10000pt";
                imagen.SetImageResource(Resource.Drawable.destacado_gasolinera);
                mascara.SetImageResource(Resource.Drawable.mascara_destacado_naranja);
				contenidoTexto.SetBackgroundColor(Color.ParseColor("#e97707"));

			}

			return view;
		}
    }
}
