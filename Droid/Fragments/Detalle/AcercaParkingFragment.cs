
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using aparcame.Models;

namespace aparcame.Droid.Fragments.Detalle
{
	public class AcercaParkingFragment : Fragment, IOnMapReadyCallback
	{
		private View rootView;
		private const string ARG_SECTION_NUMBER = "section_number";
		private GoogleMap mMap;
		private FragmentActivity myContext;
        private Parking parking;

		public static AcercaParkingFragment newInstance(int sectionNumber, Parking parking)
		{
			AcercaParkingFragment fragment = new AcercaParkingFragment();
			Bundle args = new Bundle();
			args.PutInt(ARG_SECTION_NUMBER, sectionNumber);
			fragment.Arguments = (args);
            fragment.parking = parking;
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
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{

			if (rootView == null)

			{
				rootView = inflater.Inflate(Resource.Layout.acerca_parking_fragment, container, false);
                TextView adaptadosFragment = (TextView)rootView.FindViewById(Resource.Id.adaptadosFragment);
                TextView comunesFragment = (TextView)rootView.FindViewById(Resource.Id.comunesFragment);
                TextView electrFragment = (TextView)rootView.FindViewById(Resource.Id.electricosFragment);
                TextView totalesFragment = (TextView)rootView.FindViewById(Resource.Id.totalesFragment);

                adaptadosFragment.Text = parking.minus_total_parking.ToString();
                comunesFragment.Text = parking.normal_total_parking.ToString();
                electrFragment.Text = parking.energ_total_parking.ToString();
                totalesFragment.Text = (parking.minus_total_parking + parking.normal_total_parking + parking.energ_total_parking).ToString();

			}

			return rootView;
		}


        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

			MapFragment mapFrag = (MapFragment)myContext.FragmentManager.FindFragmentById(Resource.Id.map_acerca);
            mapFrag.OnCreate(savedInstanceState);
            mapFrag.OnResume();
			mapFrag.GetMapAsync(this);

        }

        public void OnMapReady(GoogleMap googleMap)
        {
			mMap = googleMap;

            mMap.MapType = GoogleMap.MapTypeSatellite;

            LatLng tuPos = new LatLng(parking.latitud_parking, parking.longitud_parking);

			mMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(tuPos, 18));
			

		}
    }
}
