using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCursos.Shared
{
    public class Compras
    {
        [Key]
        public int CompraId { get; set; }
        
        public int UsuarioId { get; set; }

        public int CursoId { get; set; }

        public Usuarios Usuarios { get; set; }  = new Usuarios();

        public Cursos Cursos { get; set; } = new Cursos();

        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
