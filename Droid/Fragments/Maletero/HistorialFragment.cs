﻿
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
using aparcame.Droid.Adapters;

namespace aparcame.Droid.Fragments.Maletero
{
    public class HistorialFragment : Android.Support.V4.App.Fragment
    {
        private View rootView;
		private const string ARG_SECTION_NUMBER = "section_number";
        private HistorialMaleteroAdapter historialAdapter;
        private ListView listHistorial;


		public static HistorialFragment newInstance(int sectionNumber)
		{
			HistorialFragment fragment = new HistorialFragment();
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
                rootView = inflater.Inflate(Resource.Layout.historial_fragment, container, false);

                listHistorial = (ListView)rootView.FindViewById(Resource.Id.listview_historial);


                //Esto despues habra que cargar la lista de Historial real;
                List<string> historial = new List<string>();
                historial.Add("Sin contenido");
				historial.Add("Sin contenido");
				historial.Add("Sin contenido");

				historialAdapter = new HistorialMaleteroAdapter(this.Activity, historial);
                listHistorial.Adapter = historialAdapter;            
			}

			return rootView;
        }
    }
}
