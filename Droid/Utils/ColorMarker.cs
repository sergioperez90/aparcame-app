using System;
namespace aparcame.Droid.Utils
{
    public class ColorMarker
    {
        public ColorMarker()
        {
        }

        public static int calcular(int total, int disponible)
        {
            int res = -1;


            int porcentaje = (disponible * 100 / total);

            if(porcentaje >= 70)
            {
                res = Resource.Drawable.location_red;
            }
            else if(porcentaje<70 && porcentaje > 40)
            {
                res = Resource.Drawable.location_orange;
            }
            else
            {
                res = Resource.Drawable.location_green;   
            }

            return res;
        }
    }
}
