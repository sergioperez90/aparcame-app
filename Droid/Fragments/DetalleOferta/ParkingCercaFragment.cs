
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

namespace aparcame.Droid.Fragments.DetalleOferta
{
    public class ParkingCercaFragment : Fragment
    {
		private const string ARG_SECTION_NUMBER = "section_number";
		private View rootView;

		public static ParkingCercaFragment newInstance(int sectionNumber)
		{
			ParkingCercaFragment fragment = new ParkingCercaFragment();
			Bundle args = new Bundle();
			args.PutInt(ARG_SECTION_NUMBER, sectionNumber);
			fragment.Arguments = (args);
			return fragment;
		}
              
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{         
			if (rootView == null)
   			{
				rootView = inflater.Inflate(Resource.Layout.parking_cerca_fragment, container, false);            
			}

			return rootView;
		}
    }
}
