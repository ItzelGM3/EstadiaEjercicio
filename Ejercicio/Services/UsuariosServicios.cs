using System;
using System.Net.Http.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Ejercicio.Models;

namespace Ejercicio.Services
{
    public class UsuariosServicios
    {
        private readonly HttpClient _httpClient;
        private Usuarios? _empleadoActual;

        public UsuariosServicios(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Login(LoginRequest2 loginRequest)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:44361/api/Usuarios/login", loginRequest);

                if (response.IsSuccessStatusCode)
                {
                    _empleadoActual = await response.Content.ReadFromJsonAsync<Usuarios>();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al iniciar sesión: " + ex.Message);
            }
        }

        public Usuarios? GetUsuarioActual()
        {
            return _empleadoActual;
        }

        public async Task<int?> GetUsuarioIdAutenticado()
        {
            var response = await _httpClient.GetAsync("https://localhost:44361/api/Usuarios/usuarioIdAutenticado");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<int?>();
            }
            return null;
        }
    }
}