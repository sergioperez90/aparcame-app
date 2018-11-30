using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace aparcame.Droid.Adapters
{
    public class HistorialMaleteroAdapter : BaseAdapter
    {
        private Context context;
        private List<string> items; //Esto habra que cambiarlo en vez de <string> de tipo <Historial>


        public HistorialMaleteroAdapter(Context c, List<string> items)
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

        //Aqui tendra que ser de tipo Historial
        public string getItem(int position)
        {
            return items[position];
        }


        public override View GetView(int position, View view, ViewGroup viewGroup)
        {
			LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);

            view = inflater.Inflate(Resource.Layout.historial_item, viewGroup, false);
           
            //Esto habra que cambiarlo por el tipo Historial y luego sustituir los textivew por los items
            string item = getItem(position);

            TextView titulo = (TextView)view.FindViewById(Resource.Id.titulo_historial);
            TextView lugar = (TextView)view.FindViewById(Resource.Id.lugar_historial);
			TextView fecha = (TextView)view.FindViewById(Resource.Id.fecha_historial);
			TextView poblacion = (TextView)view.FindViewById(Resource.Id.poblacion_historial);
			TextView puntos = (TextView)view.FindViewById(Resource.Id.puntos_historial);
            ImageView imagen = (ImageView)view.FindViewById(Resource.Id.imagen_historial);
            
            //Esto es dinamico
            if(position == 0){
				titulo.Text = "Pizza Gratis en 2x1";
				lugar.Text = "Il Timone Express";
				poblacion.Text = "Altea (Alicante) 03590";
				fecha.Text = "15/03/2017 11:00";
				puntos.Text = "-149pt";
                imagen.SetImageResource(Resource.Drawable.pizza);

            }else if(position == 1)
            {
				titulo.Text = "Copa Gratis";
				lugar.Text = "You Chic";
				poblacion.Text = "Altea (Alicante) 03590";
				fecha.Text = "15/03/2017 11:00";
				puntos.Text = "-1149pt";
                imagen.SetImageResource(Resource.Drawable.copa);
            }
			else if (position == 2)
			{
				titulo.Text = "Gasolina gratis";
				lugar.Text = "Galp";
				poblacion.Text = "Altea (Alicante) 03590";
				fecha.Text = "15/03/2017 11:00";
				puntos.Text = "-10050pt";
                imagen.SetImageResource(Resource.Drawable.gasolinera);
			}
                     
			return view;
		}
    }
}
