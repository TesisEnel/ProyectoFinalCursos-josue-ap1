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

        [Required(ErrorMessage = "* El campo Nombre Completo es obligatorio")]
        public string? NombreCompleto { get; set; }

        [Required(ErrorMessage = "* El campo Nombre de Usuario es obligatorio")]
        public string? NombreUsuario { get; set; }

        [Required(ErrorMessage = "* El campo Email es obligatorio")]
        [EmailAddress(ErrorMessage = "* Formato de email incorrecto")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "* El campo password es obligatorio")]
        public string? Password { get; set; }

        public string? PasswordHash { get; set; } 
        public string? Salt { get; set; } 

        public int Rol { get; set; }

        [ForeignKey(("UsuarioId"))]
        public ICollection<Compras> Compras { get; set; } = new List<Compras>();

    }
}
