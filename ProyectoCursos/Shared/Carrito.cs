using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCursos.Shared
{
    public class Carrito
    {
        [Key]
        public int CompraId { get; set; }
        public int UsuarioId { get; set; }
        public int CursoId { get; set; }
        public string? NombreCurso { get; set; } 
        public DateTime Fecha { get; set; } = DateTime.Now;
        public DateTime FechaInicioCurso { get; set; } 
        public DateTime FechaFinCurso { get; set; } 
        public int PrecioCurso { get; set; }

        [ForeignKey("UsuarioId")]
        [Required]
        public Usuarios Usuario { get; set; }

        [ForeignKey("CursoId")]
        [Required]
        public Cursos Curso { get; set; }
    }

}
