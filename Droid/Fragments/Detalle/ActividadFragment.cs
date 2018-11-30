
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace aparcame.Droid.Fragments.Detalle
{
    public class ActividadFragment : Android.Support.V4.App.Fragment
    {
		private View rootView;
		private const string ARG_SECTION_NUMBER = "section_number";

		public static ActividadFragment newInstance(int sectionNumber)
		{
			ActividadFragment fragment = new ActividadFragment();
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
                rootView = inflater.Inflate(Resource.Layout.actividad_fragment, container, false);
			}

			return rootView;
        }
    }
}
