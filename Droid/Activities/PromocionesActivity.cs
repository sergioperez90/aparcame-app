
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using aparcame.Droid.Adapters;
using aparcame.Droid.Fragments.Promociones;
using Java.Lang;

namespace aparcame.Droid.Activities
{
    [Activity(Label = "PromocionesActivity", Theme = "@style/AppTheme")]
    public class PromocionesActivity : BaseActivity
    {
        ViewPager mViewPager;

		protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.promociones_activity);

			base.initToolBar();

            mViewPager = (ViewPager)FindViewById(Resource.Id.pager_promo);
			setupViewPager(mViewPager);
            TabLayout tabs = (TabLayout)FindViewById(Resource.Id.tabs_promo);
			tabs.SetupWithViewPager(mViewPager);
			mViewPager.SetCurrentItem(0, true);

        }

		/// <summary>
        /// Metodo para añadir los fragments
        /// </summary>
        /// <param name="viewPager">View pager.</param>
		private void setupViewPager(ViewPager viewPager)
		{

			SectionsPagerAdapter adapter = new SectionsPagerAdapter(SupportFragmentManager);
			adapter.addFragment(DestacadasFragment.newInstance(1), "Destacadas");
            adapter.addFragment(CercaFragment.newInstance(2), "Cerca de tí");

			viewPager.Adapter = (adapter);
		}

		/// <summary>
        /// Clase para adaptar los fragments
        /// </summary>
		public class SectionsPagerAdapter : FragmentPagerAdapter
		{
			private List<Android.Support.V4.App.Fragment> mFragments = new List<Android.Support.V4.App.Fragment>();
			private List<string> mFragmentTitles = new List<string>();

			public SectionsPagerAdapter(Android.Support.V4.App.FragmentManager fm) : base(fm)
			{
				// base(fm);
			}

			public override Android.Support.V4.App.Fragment GetItem(int position)
			{
				return mFragments[position];
			}

			public override int Count
			{
				get
				{
					return mFragments.Count;
				}
			}

			public void addFragment(Android.Support.V4.App.Fragment fragment, string title)
			{
				mFragments.Add(fragment);
				mFragmentTitles.Add(title);
			}

			public override ICharSequence GetPageTitleFormatted(int position)
			{
				string a = mFragmentTitles[position];
				Java.Lang.String javaA = new Java.Lang.String(a);

				return javaA;
			}
		}

    }
}
