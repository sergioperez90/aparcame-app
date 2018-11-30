using System;
using Android.Content;
using Android.Preferences;

namespace aparcame.Droid.Utils
{
    public class UserCredentials
    {
        public UserCredentials()
        {
        }

        //Metodos de guardado
        public static bool saveEmailUsuario(string email, Context context)
        {
            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(context);

            if (preferences != null)
            {
                ISharedPreferencesEditor editor = preferences.Edit();
                editor.PutString("EMAIL_USUARIO", email);
                return editor.Commit();
            }

            return false;
        }

        public static bool savePassUsuario(string pass, Context context)
        {
            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(context);

            if (preferences != null)
            {
                ISharedPreferencesEditor editor = preferences.Edit();
                editor.PutString("PASS_USUARIO", pass);
                return editor.Commit();
            }

            return false;
        }

        public static bool saveIdUsuario(string id, Context context)
        {
            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(context);

            if (preferences != null)
            {
                ISharedPreferencesEditor editor = preferences.Edit();
                editor.PutString("ID_USUARIO", id);
                return editor.Commit();
            }

            return false;
        }


        public static bool saveAlertaConduciendo(bool alerta, Context context)
        {
            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(context);

            if (preferences != null)
            {
                ISharedPreferencesEditor editor = preferences.Edit();
                editor.PutBoolean("ALERTA_CONDUCIENDO", alerta);
                return editor.Commit();
            }

            return false;
        }

        public static bool saveAlertaAparcado(bool alerta, Context context)
        {
            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(context);

            if (preferences != null)
            {
                ISharedPreferencesEditor editor = preferences.Edit();
                editor.PutBoolean("ALERTA_APARCADO", alerta);
                return editor.Commit();
            }

            return false;
        }


        public static bool savePermiso(bool permiso, Context context)
        {
            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(context);

            if (preferences != null)
            {
                ISharedPreferencesEditor editor = preferences.Edit();
                editor.PutBoolean("PERMISO_ACEPTADO", permiso);
                return editor.Commit();
            }

            return false;
        }

        public static bool savePrimeraVez(bool primera, Context context)
        {
            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(context);

            if (preferences != null)
            {
                ISharedPreferencesEditor editor = preferences.Edit();
                editor.PutBoolean("PRIMERA_VEZ", primera);
                return editor.Commit();
            }

            return false;
        }

       

        public static bool saveTokenJWT(string newToken, Context context)
        {
            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(context);

            if (preferences != null)
            {
                ISharedPreferencesEditor editor = preferences.Edit();
                editor.PutString("CREDENTIALS_JWT", newToken);
                return editor.Commit();
            }

            return false;
        }

        public static bool saveIdParking(string newIdParking, Context context)
        {
            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(context);

            if (preferences != null)
            {
                ISharedPreferencesEditor editor = preferences.Edit();
                editor.PutString("CREDENTIALS_ID_PARKING", newIdParking);
                return editor.Commit();
            }

            return false;
        }

        public static bool saveLatitudAparcado(string newLatitud, Context context)
        {
            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(context);

            if (preferences != null)
            {
                ISharedPreferencesEditor editor = preferences.Edit();
                editor.PutString("CREDENTIALS_LATITUD_APARCADO", newLatitud);
                return editor.Commit();
            }

            return false;
        }

        public static bool saveLongitudAparcado(string newLongitud, Context context)
        {
            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(context);

            if (preferences != null)
            {
                ISharedPreferencesEditor editor = preferences.Edit();
                editor.PutString("CREDENTIALS_LONGITUD_APARCADO", newLongitud);
                return editor.Commit();
            }

            return false;
        }

        public static bool saveLatitudUbicacion(string lat, Context context)
        {
            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(context);

            if (preferences != null)
            {
                ISharedPreferencesEditor editor = preferences.Edit();
                editor.PutString("LATITUD_TU_UBICACION", lat);
                return editor.Commit();
            }

            return false;
        }

        public static bool saveLongitudUbicacion(string lon, Context context)
        {
            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(context);

            if (preferences != null)
            {
                ISharedPreferencesEditor editor = preferences.Edit();
                editor.PutString("LONGITUD_TU_UBICACION", lon);
                return editor.Commit();
            }

            return false;
        }


        //Metodos de get
        public static bool getAlertaConduciendo(Context context)
        {

            ISharedPreferences sharedPrefs = PreferenceManager.GetDefaultSharedPreferences(context);
            bool txt = false;

            try
            {
                txt = sharedPrefs.GetBoolean("ALERTA_CONDUCIENDO", false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return txt;
        }

        public static bool getAlertaAparcado(Context context)
        {

            ISharedPreferences sharedPrefs = PreferenceManager.GetDefaultSharedPreferences(context);
            bool txt = false;

            try
            {
                txt = sharedPrefs.GetBoolean("ALERTA_APARCADO", false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return txt;
        }


        public static bool getPermiso(Context context)
        {

            ISharedPreferences sharedPrefs = PreferenceManager.GetDefaultSharedPreferences(context);
            bool txt = false;

            try
            {
                txt = sharedPrefs.GetBoolean("PERMISO_ACEPTADO", false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return txt;
        }

        public static string getIdUsuario(Context context)
        {
            
            ISharedPreferences sharedPrefs = PreferenceManager.GetDefaultSharedPreferences(context);
            String txt = null;

            try
            {
                txt = sharedPrefs.GetString("ID_USUARIO", null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return txt;
        }

        public static string getEmailUsuario(Context context)
        {

            ISharedPreferences sharedPrefs = PreferenceManager.GetDefaultSharedPreferences(context);
            String txt = null;

            try
            {
                txt = sharedPrefs.GetString("EMAIL_USUARIO", null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return txt;
        }

        public static string getPassUsuario(Context context)
        {

            ISharedPreferences sharedPrefs = PreferenceManager.GetDefaultSharedPreferences(context);
            String txt = null;

            try
            {
                txt = sharedPrefs.GetString("PASS_USUARIO", null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return txt;
        }

        public static string getTokenJWT(Context context)
        {

            ISharedPreferences sharedPrefs = PreferenceManager.GetDefaultSharedPreferences(context);
            String txt = null;

            try
            {
                txt = sharedPrefs.GetString("CREDENTIALS_JWT", null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return txt;
        }

        public static string getIdParking(Context context)
        {

            ISharedPreferences sharedPrefs = PreferenceManager.GetDefaultSharedPreferences(context);
            String txt = null;

            try
            {
                txt = sharedPrefs.GetString("CREDENTIALS_ID_PARKING", null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return txt;
        }

        public static string getLatitudAparcado(Context context)
        {

            ISharedPreferences sharedPrefs = PreferenceManager.GetDefaultSharedPreferences(context);
            String txt = null;

            try
            {
                txt = sharedPrefs.GetString("CREDENTIALS_LATITUD_APARCADO", null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return txt;
        }

        public static string getLongitudAparcado(Context context)
        {

            ISharedPreferences sharedPrefs = PreferenceManager.GetDefaultSharedPreferences(context);
            String txt = null;

            try
            {
                txt = sharedPrefs.GetString("CREDENTIALS_LONGITUD_APARCADO", null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return txt;
        }

        public static bool getPrimeraVez(Context context)
        {

            ISharedPreferences sharedPrefs = PreferenceManager.GetDefaultSharedPreferences(context);
            bool txt = true;

            try
            {
                txt = sharedPrefs.GetBoolean("PRIMERA_VEZ", true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return txt;
        }

        public static string getLatitudUbicacion(Context context)
        {

            ISharedPreferences sharedPrefs = PreferenceManager.GetDefaultSharedPreferences(context);
            string txt = null;

            try
            {
                txt = sharedPrefs.GetString("LATITUD_TU_UBICACION", null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return txt;
        }

        public static string getLongitudUbicacion(Context context)
        {

            ISharedPreferences sharedPrefs = PreferenceManager.GetDefaultSharedPreferences(context);
            string txt = null;

            try
            {
                txt = sharedPrefs.GetString("LONGITUD_TU_UBICACION", null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return txt;
        }


        //Metodos de delete
        public static void removeAll(Context context)
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(context);
            ISharedPreferencesEditor editor = prefs.Edit();

            editor.Remove("CREDENTIALS_JWT");
            editor.Remove("CREDENTIALS_ID_PARKING");
            editor.Remove("CREDENTIALS_LATITUD_APARCADO");
            editor.Remove("CREDENTIALS_LONGITUD_APARCADO");

            editor.Remove("EMAIL_USUARIO");
            editor.Remove("PASS_USUARIO");

            editor.Commit();
        }



    }
}
