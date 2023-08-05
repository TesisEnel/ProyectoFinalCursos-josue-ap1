using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoCursos.Server.DAL;
using ProyectoCursos.Shared;

namespace ProyectoCursos.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly Context _context;

        public CursosController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cursos>>> GetCursos()
        {
            if (_context.Cursos == null)
            {
                return NotFound();
            }
            return await _context.Cursos.ToListAsync();
        }

        [HttpGet("GetCategoria")]
        public async Task<ActionResult<IEnumerable<Categorias>>> GetCategoria()
        {
            var Categoria = await _context.Categorias.ToListAsync();
            if (Categoria == null || Categoria.Count == 0)
            {
                return NotFound();
            }
            return Ok(Categoria);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cursos>> GetCursos(int id)
        {
            if (_context.Cursos == null)
            {
                return NotFound();
            }
            var Cursos = await _context.Cursos.FindAsync(id);

            if (Cursos == null)
            {
                return NotFound();
            }

            return Cursos;
        }

        public bool CursosExiste(int id)
        {
            return (_context.Cursos?.Any(c => c.CursoId == id)).GetValueOrDefault();
        }

        [HttpPost]
        public async Task<ActionResult<Cursos>> PostCursos(Cursos Cursos)
        {

            if (!CursosExiste(Cursos.CursoId))
            {
 
                _context.Cursos.Add(Cursos);
            }
            else 
            {
                _context.Cursos.Update(Cursos);
            }
            await _context.SaveChangesAsync();
            return Ok(Cursos);
                
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteCursos(int id)
        {
            if (_context.Cursos == null)
            {
                return NotFound();
            }
            var Cursos = await _context.Cursos.FindAsync(id);
            if (Cursos == null)
            {
                return NotFound();
            }
            _context.Cursos.Remove(Cursos);
            await _context.SaveChangesAsync();
            return NoContent();
        }

       
    }
}

