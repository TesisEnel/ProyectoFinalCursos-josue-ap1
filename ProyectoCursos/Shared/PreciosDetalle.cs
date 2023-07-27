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

        [Required(ErrorMessage = "* El campo Precios  es obligatorio")]
        [Range(minimum:1,maximum:1000000, ErrorMessage = "Debe estar en un rango permitido")]
        public Precios Precios { get; set; } = new Precios();
    }
}
