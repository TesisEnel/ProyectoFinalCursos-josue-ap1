using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCursos.Shared
{
    public  class CursosDetalle
    {
        [Key]

        public int CursosDetalleId { get; set; }

        public int UsuarioId { get; set; }

        public Cursos Cursos = new Cursos();
    }
}
