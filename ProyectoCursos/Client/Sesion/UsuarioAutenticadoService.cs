using Microsoft.JSInterop;
using ProyectoCursos.Shared;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProyectoCursos.Client.Sesion
{
    public interface IUsuarioAutenticadoService
    {
        Usuarios Usuario { get; set; }
        Task GuardarUsuarioEnLocalStorageAsync();
        Task CargarUsuarioDesdeLocalStorageAsync();
    }

    public class UsuarioAutenticadoService : IUsuarioAutenticadoService
    {
        private readonly IJSRuntime _jsRuntime;
        private const string StorageKey = "UsuarioAutenticado";

        public UsuarioAutenticadoService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public Usuarios Usuario { get; set; }

        public async Task GuardarUsuarioEnLocalStorageAsync()
        {
            var json = JsonSerializer.Serialize(Usuario);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
        }

        public async Task CargarUsuarioDesdeLocalStorageAsync()
        {
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", StorageKey);
            Usuario = string.IsNullOrEmpty(json) ? null : JsonSerializer.Deserialize<Usuarios>(json);
        }
    }
}
