using System;
using System.Collections.Generic;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using aparcame.Droid.Fragments;

namespace aparcame.Droid.Adapters
{
    public class AnuncioFragmentAdapter : FragmentPagerAdapter
	{
		readonly List<Fragment> fragmentList = new List<Fragment>();

		public AnuncioFragmentAdapter(FragmentManager fragmentManager) : base(fragmentManager)
		{
		}

		public override int Count
		{
			get
			{
				return fragmentList.Count;
			}
		}

		public override Fragment GetItem(int position)
		{
			return fragmentList[position];
		}

		public void addFragmentView(Func<LayoutInflater, ViewGroup, Bundle, View> fragmentView)
		{
			fragmentList.Add(new AnuncioFragment(fragmentView));
		}
	}
}
