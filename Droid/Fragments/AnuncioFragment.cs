﻿using System;
using Android.OS;
using Android.Views;
namespace aparcame.Droid.Fragments
{
	public class AnuncioFragment : Android.Support.V4.App.Fragment
	{
		readonly Func<LayoutInflater, ViewGroup, Bundle, View> view;

		public AnuncioFragment(Func<LayoutInflater, ViewGroup, Bundle, View> view)
		{
			this.view = view;
		}

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView(inflater, container, savedInstanceState);
			return view(inflater, container, savedInstanceState);
		}
	}
}
