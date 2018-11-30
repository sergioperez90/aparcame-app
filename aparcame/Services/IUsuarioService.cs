using System;
using System.Threading.Tasks;
using aparcame.Models;

namespace aparcame.Services
{
    public interface IUsuarioService
    {
        Task<bool> Registro(string nombre, string email, string pass);
        Task<Usuario> Login(string email, string pass);
        Task<bool> AddVehiculo(int tipo_vehiculo, string id_usuario);
        Task<bool> SumarPuntos(string puntos, string id_usuario);
        Task <Vehiculo> DameVehiculoUsuario(string id_usuario);

    }
}
