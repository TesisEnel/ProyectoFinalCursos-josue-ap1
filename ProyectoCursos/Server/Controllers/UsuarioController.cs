using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ProyectoCursos.Server.DAL;
using ProyectoCursos.Shared;
using System.Linq;


namespace ProyectoCursos.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly Context _context;

        public UsuarioController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuarios>>> GetClientes()
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            return await _context.Usuarios.ToListAsync();
        }

        [HttpGet("/GetRoles")]
        public async Task<ActionResult<IEnumerable<Roles>>> GetRoles()
        {
            if (_context.Roles == null)
            {
                return NotFound();
            }
            return await _context.Roles.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuarios>> GetCursos(int id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var Usuarios = await _context.Usuarios.FindAsync(id);

            if (Usuarios == null)
            {
                return NotFound();
            }

            return Usuarios;
        }

        public bool UsuarioExiste(int id)
        {
            return (_context.Usuarios?.Any(u => u.UsuarioId == id)).GetValueOrDefault();
        }
        [HttpPost]

        public async Task<ActionResult<Usuarios>> PostUsuario(Usuarios Usuarios)
        {
            if (!UsuarioExiste(Usuarios.UsuarioId))
                _context.Usuarios.Add(Usuarios);
            else
                _context.Usuarios.Update(Usuarios);
            await _context.SaveChangesAsync();
            return Ok(Usuarios);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var Usuarios = await _context.Usuarios.FindAsync(id);
            if (Usuarios == null)
            {
                return NotFound();
            }
            _context.Usuarios.Remove(Usuarios);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST api/authentication/login
        [HttpPost("AuthenticateUser")]
        public ActionResult<bool> AuthenticateUser([FromBody] Usuarios loginModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid login data");
                }

                // Busca el usuario en la base de datos por su email
                var user = _context.Usuarios.FirstOrDefault(u => u.Email == loginModel.Email);

                if (user == null)
                {
                    // Si el usuario no se encuentra, el inicio de sesión falla.
                    return NotFound("User not found.");
                }

                // Verifica la contraseña
                bool isPasswordValid = VerifyPassword(loginModel.Password, user.PasswordHash, user.Salt);

                if (!isPasswordValid)
                {
                    return BadRequest("Invalid password.");
                }

                // Inicio de sesión exitoso
                return Ok(true);
            }
            catch (Exception ex)
            {
                // Si ocurre un error, devuelve un código de error 500 con un mensaje de error.
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        private bool VerifyPassword(string password, string savedPasswordHash, string salt)
        {
            // Genera el hash de la contraseña proporcionada con el salt almacenado en la base de datos
            string hashedPassword = PasswordHashHelper.GetHashedPassword(password, salt);

            // Compara el hash generado con el hash almacenado en la base de datos
            return hashedPassword == savedPasswordHash;
        }

    }
}
