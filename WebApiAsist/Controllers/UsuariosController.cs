using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPiAsist.Data;
using WebAPiAsist.Models;
using System.Threading.Tasks;
using System;

namespace WebApiAsist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private static int? _usuarioIdAutenticado; 
        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Cambios en el método Login
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginRequest2 loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Nombre) || string.IsNullOrEmpty(loginRequest.Contrasena))
            {
                return BadRequest("Invalid client request");
            }

            var user = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Nombre == loginRequest.Nombre && u.Contrasena == loginRequest.Contrasena);

            if (user == null)
            {
                return Unauthorized("Usuario o contraseña incorrectos");
            }

            // Guardar el ID del usuario 
            _usuarioIdAutenticado = user.Id;

            var asistencia = await _context.App
                .Where(a => a.UsuarioId == user.Id && a.FechaSalida == null)
                .OrderByDescending(a => a.FechaEntrada)
                .FirstOrDefaultAsync();

            if (asistencia != null)
            {
                // Registrar la hora de salida
                asistencia.FechaSalida = DateTime.Now;
                _context.App.Update(asistencia);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Registrar la hora de entrada
                var nuevaAsistencia = new App
                {
                    UsuarioId = user.Id,
                    FechaEntrada = DateTime.Now,
                };

                _context.App.Add(nuevaAsistencia);
                await _context.SaveChangesAsync();
            }

            var response = new LoginResponse2
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellidos = user.Apellidos,
                Contrasena = user.Contrasena
            };

            return Ok(response);
        }


        [HttpGet]
        [Route("usuarioIdAutenticado")]
        public ActionResult GetUsuarioIdAutenticado()
        {
            if (_usuarioIdAutenticado.HasValue)
            {
                return Ok(_usuarioIdAutenticado.Value);
            }
            return Unauthorized("No hay usuario autenticado.");
        }

        // Historial
        [HttpGet]
        [Route("historial")]
        public async Task<ActionResult> GetHistorial()
        {
            if (!_usuarioIdAutenticado.HasValue)
            {
                return Unauthorized("No hay usuario autenticado.");
            }

            var historial = await _context.App.Where(a => a.UsuarioId == _usuarioIdAutenticado.Value).ToListAsync();
            return Ok(historial);
        }

        [HttpPost]
        [Route("entrada")]
        public async Task<ActionResult> RegistrarEntrada()
        {
            if (!_usuarioIdAutenticado.HasValue)
            {
                return Unauthorized("No hay usuario autenticado.");
            }

            var entrada = new App
            {
                UsuarioId = _usuarioIdAutenticado.Value,
                FechaEntrada = DateTime.Now,
            };
            _context.App.Add(entrada);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("salida")]
        public async Task<ActionResult> RegistrarSalida()
        {
            if (!_usuarioIdAutenticado.HasValue)
            {
                return Unauthorized("No hay usuario autenticado.");
            }

            var entrada = await _context.App.Where(a => a.UsuarioId == _usuarioIdAutenticado.Value && a.FechaSalida == null)
                                             .OrderByDescending(a => a.FechaEntrada)
                                             .FirstOrDefaultAsync();
            if (entrada != null)
            {
                entrada.FechaSalida = DateTime.Now;
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest("No hay entradas pendientes de salida.");
        }
    }
}