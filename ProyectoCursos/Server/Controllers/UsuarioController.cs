using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ProyectoCursos.Server.DAL;
using ProyectoCursos.Shared;
using System.Linq;
using System.Linq.Expressions;
using SQLitePCL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        public async Task<ActionResult<IEnumerable<Usuarios>>> GetUsuarios()
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            return await _context.Usuarios.ToListAsync();
        }

        [HttpGet("FilterUsuarios")]
        public async Task<ActionResult<IEnumerable<Usuarios>>> FilterUsuarios(string filtro, string criterio, DateTime? fechaNacimiento)
        {
            var query = _context.Usuarios.AsQueryable();

            if (!string.IsNullOrEmpty(criterio))
            {
                switch (filtro)
                {
                    case "1":
                        query = query.Where(u => u.UsuarioId.ToString() == criterio);
                        break;
                    case "2":
                        query = query.Where(u => u.NombreCompleto.Contains(criterio));
                        break;
                    case "3":
                        if (fechaNacimiento.HasValue)
                        {
                            var fecha = fechaNacimiento.Value;
                            query = query.Where(u => u.FechaNacimiento.Year == fecha.Year &&
                                                     u.FechaNacimiento.Month == fecha.Month &&
                                                     u.FechaNacimiento.Day == fecha.Day);
                        }
                        break;
                    case "4":
                        query = query.Where(u => u.NombreUsuario.Contains(criterio));
                        break;
                    case "5":
                        query = query.Where(u => u.Email.Contains(criterio));
                        break;
                    case "6":
                        if (int.TryParse(criterio, out var rolId))
                        {
                            query = query.Where(u => u.Rol == rolId);
                        }
                        break;
                }
            }

            var filteredUsers = await query.ToListAsync();
            return Ok(filteredUsers);
        }


        [HttpGet("GetRoles")]
        public async Task<ActionResult<IEnumerable<Roles>>> GetRoles()
        {

            var roles = await _context.Roles.ToListAsync();
            if (roles == null || roles.Count == 0)
            {
                return NotFound();
            }
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuarios>> GetUsuarios(int id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var Usuarios = _context.Usuarios
                .Where(u => u.UsuarioId == id)
                .Include(c => c.Compras)
                .AsNoTracking()
                .SingleOrDefault();

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
        public async Task<ActionResult<Usuarios>> GetUsuarios(Usuarios Usuarios)
        {
            if (!UsuarioExiste(Usuarios.UsuarioId))
                _context.Usuarios.Add(Usuarios);
            else
                _context.Usuarios.Update(Usuarios);
            await _context.SaveChangesAsync();
            return Ok(Usuarios);
        }

        [HttpGet("GetByUsername/{username}")]
        public async Task<ActionResult<Usuarios>> GetUserByUsername(string username)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == username);
            if (user == null)
            {
                return NotFound();
            }
            return user;
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

        [HttpPost("AuthenticateUser")]
        public ActionResult<Usuarios> AuthenticateUser([FromBody] Login loginModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid login data");
                }
                var user = _context.Usuarios.FirstOrDefault(u => u.Email == loginModel.Email);
                if (user == null)
                {

                    return NotFound("User not found.");
                }
                if (user.Password == loginModel.Password)
                {
                    return user;
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");

            }
        }

    }
}