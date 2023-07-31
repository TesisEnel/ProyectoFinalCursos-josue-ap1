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
        public async Task<ActionResult<Usuarios>> GetUsuarios(Usuarios Usuarios)
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
