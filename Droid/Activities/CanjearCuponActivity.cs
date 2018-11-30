
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

namespace aparcame.Droid.Activities
{
    [Activity(Label = "CanjearCuponActivity", Theme = "@style/AppTheme")]
    public class CanjearCuponActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_cupon);

			base.initToolBar();
        }
    }
}
