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

namespace aparcame.Droid.Fragments.Maletero
{
    public class TusPuntosFragment : Android.Support.V4.App.Fragment
    {
		private View rootView;
		private const string ARG_SECTION_NUMBER = "section_number";
		public static TusPuntosFragment newInstance(int sectionNumber)
		{
			TusPuntosFragment fragment = new TusPuntosFragment();
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
                rootView = inflater.Inflate(Resource.Layout.tusPuntos_fragment, container, false);

                TextView puntos = (TextView)rootView.FindViewById(Resource.Id.puntos_actual);
                puntos.Text = Constants.usuario.puntos_usuario.ToString();            
            }

            return rootView;
        }
    }
}
