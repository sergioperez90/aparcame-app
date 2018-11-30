
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
    [Activity(Label = "PreRegisterActivity", Theme = "@style/AppTheme")]

    public class PreRegisterActivity : Activity
    {
        public static Activity fa;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.pre_register_activity);

            //Inicializamos las variables
            fa = this;

            //Inicializamos las vistas
            Button iniciarSesion = (Button)FindViewById(Resource.Id.iniciar_sesion);
            Button registro = (Button)FindViewById(Resource.Id.registro);
           
            //Eventos de botones
            iniciarSesion.Click += delegate {                
                StartActivity(new Intent(Application.Context, typeof(LoginActivity)));
            };

            registro.Click += delegate {            
                StartActivity(new Intent(Application.Context, typeof(RegistroUsuarioActivity)));            
            };


        }
    }
}
