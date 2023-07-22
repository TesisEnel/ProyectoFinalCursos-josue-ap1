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
    public class Usuarios
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "* El campo nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "* El campo apellido es obligatorio")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "* El campo Email es obligatorio")]
        [EmailAddress(ErrorMessage = "* Formato de email incorrecto")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* El campo password es obligatorio")]
        public string Password { get; set; }

        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }

        [ForeignKey(("CursosDetalle"))]
        public ICollection<Cursos> ListaCursos { get; set; } = new List<Cursos>();

    }
}
