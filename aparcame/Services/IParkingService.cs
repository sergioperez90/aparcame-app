using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using aparcame.Models;

namespace aparcame.Services
{
    public interface IParkingService
    {
        Task<List<Parking>> DameParkings();
        Task<Parking> DameParkingPorId(string id);
        Task<int> ComprobarParking(double latitud, double longitud, string cp);
        Task<bool> RestarSitio(int id);
        Task<bool> SumarSitio(int id, int tipo);      
    }
}
