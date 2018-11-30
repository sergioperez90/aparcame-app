
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
using aparcame.Droid.Utils;
using aparcame.Models;
using aparcame.Services;

namespace aparcame.Droid.Activities
{
    [Activity(Label = "LoginActivity", Theme = "@style/AppTheme")]
    public class LoginActivity : BaseActivity
    {
        private IUsuarioService _usuarioService;
        private EditText email, pass;
        private TextView campos;
        private Button login;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.login_activity);

            base.initToolBar();

            //Inicializamos variables
            _usuarioService = new UsuarioService();


            //Iniciamos las vistas
            email = (EditText)FindViewById(Resource.Id.email_login);
            pass = (EditText)FindViewById(Resource.Id.pass_login);
            campos = (TextView)FindViewById(Resource.Id.campos_obligatorios);
            login = (Button)FindViewById(Resource.Id.loguearse);


            //Eventos botones
            login.Click += delegate {
                Login();

            };

        }

        /// <summary>
        /// Metodo para hacer login
        /// </summary>
        public async void Login()
        {
            if (email.Text != "" && pass.Text != "")
            {
                ProgressDialog pr = new Android.App.ProgressDialog(this);
                pr.SetMessage("Iniciando sesión...");
                pr.SetCancelable(false);
                pr.Show();

                campos.Visibility = ViewStates.Invisible;
                Usuario usuario = await _usuarioService.Login(email.Text, pass.Text);

                if (usuario != null)
                {
                    ComprobarVehiculo();
                    UserCredentials.saveEmailUsuario(email.Text, this);
                    UserCredentials.savePassUsuario(pass.Text, this);
                    UserCredentials.saveIdUsuario(usuario.id_usuario.ToString(), this);
                    UserCredentials.saveTokenJWT(usuario.Token, this);

                    pr.Dismiss();
                    StartActivity(new Intent(Application.Context, typeof(MainActivity)));
                    PreRegisterActivity.fa.Finish();
                    Finish();

                }
                else
                {
                    Toast toast = Toast.MakeText(this, "Ha ocurrido un error al iniciar sesión", ToastLength.Long); toast.Show();
                    pr.Dismiss();
                }            
            }
            else
            {
                campos.Visibility = ViewStates.Visible;
            }

            //Comprobamos si los campos son vacios o no para mostrar el color
            ComprobarCampos();
        }

        /// <summary>
		/// Metodo que comprueba el vehiculo del usuario
        /// </summary>
        private async void ComprobarVehiculo()
        {
            Vehiculo vehiculo = await _usuarioService.DameVehiculoUsuario(Constants.usuario.id_usuario.ToString());

            if (vehiculo != null)
            {
                Constants.vehiculo = vehiculo;
            }

        }

        /// <summary>
        /// Metodo q comprueba q los campos esten llenos
        /// </summary>
        public void ComprobarCampos()
        {

            if (email.Text == "")
            {
                email.SetBackgroundResource(Resource.Drawable.border_bottom_rojo);
                email.SetError("El email es obligatorio", null);
            }

            if (pass.Text == "")
            {
                pass.SetBackgroundResource(Resource.Drawable.border_bottom_rojo);
                pass.SetError("La contraseña es obligatoria", null);
            }
        }
    }
}
