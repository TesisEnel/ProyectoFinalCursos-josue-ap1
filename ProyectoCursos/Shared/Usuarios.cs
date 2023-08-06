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

        public DateTime FechaNacimiento { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "* El campo Nombre de Usuario es obligatorio")]
        public string? NombreUsuario { get; set; }

        [NotMapped]
        public string? NombreUsuarioError { get; set; }

        [Required(ErrorMessage = "* El campo Email es obligatorio")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$", ErrorMessage = "* Formato de email incorrecto")]
        [EmailAddress(ErrorMessage = "* Formato de email incorrecto")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "* El campo password es obligatorio")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,16}$", ErrorMessage = "La contraseña debe tener al menos 8 caracteres, incluyendo una mayúscula, un número y un símbolo.")]
        public string? Password { get; set; }

        [NotMapped]
        public Carrito Carrito { get; set; } = new Carrito();
        public int Rol { get; set; }

        [ForeignKey(("UsuarioId"))]
        public ICollection<Compras> Compras { get; set; } = new List<Compras>();

    }
}