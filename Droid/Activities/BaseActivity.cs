
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Support.V7.Widget;

namespace aparcame.Droid.Activities
{
	[Activity(Label = "BaseActivity")]
	public class BaseActivity : AppCompatActivity
	{
		Toolbar toolbar;

        /// <summary>
        /// Metodo para iniciar la toolbar
        /// </summary>
		public void initToolBar()
		{
			toolbar = (Toolbar)FindViewById(Resource.Id.toolbar);
			SetSupportActionBar(toolbar);
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);
			SupportActionBar.SetDisplayShowHomeEnabled(true);
			SupportActionBar.SetDisplayShowTitleEnabled(false);

			if (Build.VERSION.SdkInt >= Build.VERSION_CODES.Lollipop)
                SupportActionBar.SetHomeAsUpIndicator(Resources.GetDrawable(Resource.Mipmap.ic_arrow_back_white_24dp, null));
			else
				SupportActionBar.SetHomeAsUpIndicator(Resources.GetDrawable(Resource.Mipmap.ic_arrow_back_white_24dp));
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
                case Android.Resource.Id.Home:
					OnBackPressed();
					return true;
			}

			return base.OnOptionsItemSelected(item);
		}
	}
}
