using ProyectoCursos.Shared;

namespace ProyectoCursos.Client.Sesion
{
    public interface IUsuarioAutenticadoService
    {
        Usuarios Usuario { get; set; }
    }
    public class UsuarioAutenticadoService : IUsuarioAutenticadoService 
    {
        public Usuarios Usuario { get; set; }
    }
}
