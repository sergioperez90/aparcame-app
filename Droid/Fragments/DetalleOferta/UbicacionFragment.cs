
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace aparcame.Droid.Fragments.DetalleOferta
{

	public class UbicacionFragment : Fragment, IOnMapReadyCallback
    {
		private const string ARG_SECTION_NUMBER = "section_number";
		private View rootView;
		private GoogleMap mMap;
		private FragmentActivity myContext;

		public static UbicacionFragment newInstance(int sectionNumber)
		{
			UbicacionFragment fragment = new UbicacionFragment();
			Bundle args = new Bundle();
			args.PutInt(ARG_SECTION_NUMBER, sectionNumber);
			fragment.Arguments = (args);
			return fragment;
		}

		public override void OnAttach(Android.App.Activity activity)
		{
			myContext = (FragmentActivity)activity;
			base.OnAttach(activity);
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
				rootView = inflater.Inflate(Resource.Layout.ubicacion_fragment, container, false);
				FloatingActionButton fab = rootView.FindViewById<FloatingActionButton>(Resource.Id.fab);
            
                fab.Click += delegate {
                    
                };            
			}

			return rootView;
		}

		public override void OnViewCreated(View view, Bundle savedInstanceState)
		{
			base.OnViewCreated(view, savedInstanceState);
                     
			MapFragment mapFrag = (MapFragment)myContext.FragmentManager.FindFragmentById(Resource.Id.map_ubicacion_oferta);
			mapFrag.OnCreate(savedInstanceState);
			mapFrag.OnResume();
			mapFrag.GetMapAsync(this);

		}

		public void OnMapReady(GoogleMap googleMap)
		{
			mMap = googleMap;

			mMap.MapType = GoogleMap.MapTypeSatellite;

			LatLng tuPos = new LatLng(38.387604, -0.512030); //Habra que cojer la posicion del parking

			mMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(tuPos, 18));         
		}
    }
}
