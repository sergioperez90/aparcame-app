
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    [Activity(Label = "RegistroUsuarioActivity", Theme = "@style/AppTheme")]
    public class RegistroUsuarioActivity : BaseActivity
    {
        private IUsuarioService _usuarioService;
        private EditText nombre, email, pass;
        private TextView campos;
        private Button registro;
        private Spinner tipoVehiculo;
        private int tVehiculo;
        private ProgressDialog pr;
        private Switch aceptasTerminos;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_registro_usuario);

            base.initToolBar();

            //Inicializamos variables
            _usuarioService = new UsuarioService();


            //Iniciamos las vistas
            nombre = (EditText)FindViewById(Resource.Id.nombre_registro);
            email = (EditText)FindViewById(Resource.Id.email_registro);
            pass = (EditText)FindViewById(Resource.Id.pass_registro);
            campos = (TextView)FindViewById(Resource.Id.campos_obligatorios);
            registro = (Button)FindViewById(Resource.Id.crear_cuenta);
            tipoVehiculo = (Spinner)FindViewById(Resource.Id.tipoVehiculo);

            //Eventos botones
            registro.Click += delegate {
                Registrarse();
               
            };

            //Spinner tipo de vehiculo
            tipoVehiculo.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.cars_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            tipoVehiculo.Adapter = adapter;

            //Switch terminos
            aceptasTerminos = (Switch)FindViewById(Resource.Id.switchTerminos);
            TextView terminos = (TextView)FindViewById(Resource.Id.txtTerminos);


            terminos.Click += delegate {
                var uri = Android.Net.Uri.Parse("http://www.aparcame.com/terminos"); //todo cambiar terminos
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);
            };
        }

        /// <summary>
        /// Spinners the item selected.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            tVehiculo = e.Position + 1;

        }

        /// <summary>
        /// Metodo para registro
        /// </summary>
        public async void Registrarse()
        {
            if (nombre.Text != "" && email.Text != "" && pass.Text != "" && Utils.Utils.comprobarEmail(email.Text))
            {
                if (aceptasTerminos.Checked)
                {               
                    pr = new Android.App.ProgressDialog(this);
                    pr.SetMessage("Registrando...");
                    pr.SetCancelable(false);
                    pr.Show();

                    campos.Visibility = ViewStates.Invisible;
                    bool registrado = await _usuarioService.Registro(nombre.Text, email.Text, pass.Text);

                    if (registrado)
                    {
                        Login(); //Hacemos el login para obtener los datos
                    }
                    else
                    {
                        Toast toast = Toast.MakeText(this, "Ha ocurrido un error al registrarte", ToastLength.Long); toast.Show();
                        pr.Dismiss();
                    }

                }
                else
                {
					Toast toast = Toast.MakeText(this, "Debes aceptar los términos y condiciones", ToastLength.Long); toast.Show();
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
        /// Metodo para hacer login
        /// </summary>
        public async void Login()
        {
            Usuario usuario = await _usuarioService.Login(email.Text, pass.Text);

            if(usuario != null)
            {
                CrearVehiculo();
                UserCredentials.saveEmailUsuario(email.Text, this);
                UserCredentials.savePassUsuario(pass.Text, this);
                UserCredentials.saveIdUsuario(usuario.id_usuario.ToString(), this);
                UserCredentials.saveTokenJWT(usuario.Token, this);
                pr.Dismiss();
                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
                PreRegisterActivity.fa.Finish();
				Finish();
            }

        }

        /// <summary>
        /// Metodo para crear el vehiculo
        /// </summary>
        public async void CrearVehiculo()
        {
            bool creado = await _usuarioService.AddVehiculo(tVehiculo, Constants.usuario.id_usuario.ToString());

            if(creado)
            {
                Vehiculo vehiculo = await _usuarioService.DameVehiculoUsuario(Constants.usuario.id_usuario.ToString());

                if (vehiculo != null)
                {
                    Constants.vehiculo = vehiculo;
                }
            }

        }

        /// <summary>
        /// Metodo para comprobar q los campos esten llenos
        /// </summary>
        public void ComprobarCampos()
        {
            if (nombre.Text == "")
            {
                nombre.SetBackgroundResource(Resource.Drawable.border_bottom_rojo);
                nombre.SetError("El nombre es obligatorio", null);
            }

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

            if (!Utils.Utils.comprobarEmail(email.Text))
            {
                email.SetBackgroundResource(Resource.Drawable.border_bottom_rojo);
                email.SetError("Formato de email no válido", null);
                campos.Visibility = ViewStates.Visible;
                campos.Text = "Todos los campos son obligatorios / formado de email no válido";
            }
        }


    }
}
