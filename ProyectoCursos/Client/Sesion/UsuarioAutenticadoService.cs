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
    }
}
