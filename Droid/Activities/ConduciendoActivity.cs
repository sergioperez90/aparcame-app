
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
    [Activity(Label = "ConduciendoActivity", Theme = "@style/AppTheme")]
    public class ConduciendoActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_conduciendo);

            ImageView cerrarFiltro = (ImageView)FindViewById(Resource.Id.cerrar_filtro);
            Button entendido = (Button)FindViewById(Resource.Id.btnEntendido);

            cerrarFiltro.Click += delegate {
                Finish();
            };

            entendido.Click += delegate {
                Finish();
            };
        }
    }
}
