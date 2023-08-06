using Microsoft.JSInterop;
using ProyectoCursos.Shared;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProyectoCursos.Client.Sesion
{
    public interface IUsuarioAutenticadoService
    {
        Task CargarUsuarioAsync();
        Usuarios Usuario { get; set; }
        void AgregarCursoAlCarrito(Cursos curso);
        List<Cursos> ObtenerCursosDelCarrito();
        void RemoverCursoDelCarrito(Cursos curso);

    }

    public class UsuarioAutenticadoService : IUsuarioAutenticadoService
    {
        private readonly IJSRuntime jsRuntime;

        public UsuarioAutenticadoService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        private Usuarios _usuario;
        public Usuarios Usuario
        {
            get => _usuario;
            set
            {
                _usuario = value;
                GuardarUsuarioEnLocalStorageAsync(value);
            }
        }

        public async Task CargarUsuarioAsync()
        {
            var userJson = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "user");
            if (!string.IsNullOrEmpty(userJson))
            {
                _usuario = JsonSerializer.Deserialize<Usuarios>(userJson);
            }
        }

        private async Task GuardarUsuarioEnLocalStorageAsync(Usuarios usuario)
        {
            var userJson = JsonSerializer.Serialize(usuario);
            await jsRuntime.InvokeVoidAsync("localStorage.setItem", "user", userJson);
        }

        public void AgregarCursoAlCarrito(Cursos curso)
        {
            if (Usuario != null)
            {
                Usuario.Carrito.Cursos.Add(curso);
                GuardarUsuarioEnLocalStorageAsync(Usuario);
            }
        }

        public List<Cursos> ObtenerCursosDelCarrito()
        {
            return Usuario?.Carrito?.Cursos ?? new List<Cursos>();
        }

        public void RemoverCursoDelCarrito(Cursos curso)
        {
            if (Usuario != null && curso != null && Usuario.Compras != null)
            {
                var compraAEliminar = Usuario.Compras.FirstOrDefault(c => c.CursoId == curso.CursoId);

                if (compraAEliminar != null)
                {
                    Usuario.Compras.Remove(compraAEliminar);
                    GuardarUsuarioEnLocalStorageAsync(Usuario);
                }
            }
        }
    }
}
