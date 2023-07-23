using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCursos.Shared
{
    public class PreciosDetalle
    {
        [Key]
        public int PreciosDetalleId { get; set; }
        public int CursoId { get; set; }

        public Precios Precios { get; set; } = new Precios();
    }
}
