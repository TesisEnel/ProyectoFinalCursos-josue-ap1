using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoCursos.Server.DAL;
using ProyectoCursos.Shared;

namespace ProyectoCursos.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly Context _context;

        public CategoriaController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categorias>>> GetCategorias()
        {
            if (_context.Categorias == null)
            {
                return NotFound();
            }
            return await _context.Categorias.ToListAsync();
        }
        [HttpGet("GetNiveles")]
        public async Task<ActionResult<IEnumerable<Niveles>>> GetNiveles()
        {

            var Niveles = await _context.Niveles.ToListAsync();
            if (Niveles == null || Niveles.Count == 0)
            {
                return NotFound();
            }
            return Ok(Niveles);
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<Categorias>> GetCategorias(int id)
        {
            if (_context.Categorias == null)
            {
                return NotFound();
            }
            var Categorias = await _context.Categorias.FindAsync(id);

            if (Categorias == null)
            {
                return NotFound();
            }

            return Categorias;
        }

        public bool CategoriasExiste(int id)
        {
            return (_context.Categorias?.Any(c => c.CategoriaId == id)).GetValueOrDefault();
        }

        [HttpPost]
        public async Task<ActionResult<Categorias>> PostCategorias(Categorias Categorias)
        {

            if (!CategoriasExiste(Categorias.CategoriaId))
            {

                _context.Categorias.Add(Categorias);
            }
            else
            {
                _context.Categorias.Update(Categorias);
            }
            await _context.SaveChangesAsync();
            return Ok(Categorias);

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteCategorias(int id)
        {
            if (_context.Categorias == null)
            {
                return NotFound();
            }
            var Categorias = await _context.Categorias.FindAsync(id);
            if (Categorias == null)
            {
                return NotFound();
            }
            _context.Categorias.Remove(Categorias);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
