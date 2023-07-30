using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoCursos.Shared
{
    public class Cursos
    {
        [Key]
        public int CursoId { get; set; }

        [Required(ErrorMessage = "* El campo Nombre  es obligatorio")]
        public string? NombreCurso { get; set; }

        [Required(ErrorMessage = "* El campo Descripcion  es obligatorio")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "* El campo Ruta Imagen es obligatorio")]
        public byte[]? RutaImagen { get; set; } 

        [Required(ErrorMessage = "* El campo Fecha Inicio es obligatorio")]
        public DateTime FechaAlta { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "* El campo Programa es obligatorio")]
        public string? Programa { get; set; }

        [Required(ErrorMessage = "* El campo fecha Fin es obligatorio")]
        public DateTime FechaBaja { get; set; } = DateTime.Now;


        [ForeignKey(("CursoId"))]
        public ICollection <PreciosDetalle> PreciosDetalles { get; set; } = new List<PreciosDetalle>();
    }

    public class PreciosDetalle
    {
        [Key]
        public int PreciosDetalleId { get; set; }
        public int CursoId { get; set; }

        [Required(ErrorMessage = "* El campo Precios  es obligatorio")]
        public int Precio { get; set; }

        [Required(ErrorMessage = "* El campo Fecha Inicio es obligatorio")]
        public DateTime FechaInicio { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "* El campo fecha Fin es obligatorio")]
        public DateTime FechaFin { get; set; } = DateTime.Now;

    }
}
