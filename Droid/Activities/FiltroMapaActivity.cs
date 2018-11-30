
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
    [Activity(Label = "FiltroMapaActivity", Theme = "@style/AppTheme")]
    public class FiltroMapaActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.filtro_mapa_activity);

            ImageView cerrarFiltro = (ImageView)FindViewById(Resource.Id.cerrar_filtro);

            cerrarFiltro.Click += delegate {
                Finish();
            };         
		}
    }
}
