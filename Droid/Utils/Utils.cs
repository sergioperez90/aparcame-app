using System;
namespace aparcame.Droid.Utils
{
    public class Utils
    {
        public static bool comprobarEmail(string email)
        {
            return Android.Util.Patterns.EmailAddress.Matcher(email).Matches();
        }
    }
}
