using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoCursos.Server.DAL;
using ProyectoCursos.Shared;

namespace ProyectoCursos.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly Context _context;

        public ComprasController(Context context)
        {
            _context = context;
        }

        public bool ComprasExiste(int id)
        {
            return (_context.Compras?.Any(c => c.CompraId == id)).GetValueOrDefault();
        }

        [HttpPost]
        public async Task<ActionResult<Carrito>> PostCompras(int cursoId, int usuarioId, Carrito Compra)
        {
            var curso = await _context.Cursos.FindAsync(cursoId);
            var usuario = await _context.Usuarios.FindAsync(usuarioId);

            if (curso == null || usuario == null)
            {
                return BadRequest("Curso o usuario no encontrados.");
            }

            Compra.Curso = curso;
            Compra.Usuario = usuario;

            if (!ComprasExiste(Compra.CompraId))
            {
                _context.Compras.Add(Compra);
            }
            else
            {
                _context.Compras.Update(Compra);
            }

            await _context.SaveChangesAsync();
            return Ok(Compra);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteCompras(int id)
        {
            if (_context.Compras == null)
            {
                return NotFound();
            }
            var carrito = await _context.Compras.FindAsync(id);
            if (carrito == null)
            {
                return NotFound();
            }
            _context.Compras.Remove(carrito);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
