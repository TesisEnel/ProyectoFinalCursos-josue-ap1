using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoCursos.Server.DAL;
using ProyectoCursos.Shared;

namespace ProyectoCursos.Server.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<Compras>> PostCompras(Compras Compras)
        {
            if (!ComprasExiste(Compras.CompraId))
            {
                _context.Compras.Add(Compras);
            }
            else
            {
                _context.Compras.Update(Compras);
            }
            await _context.SaveChangesAsync();
            return Ok(Compras);
        }

    }
}
