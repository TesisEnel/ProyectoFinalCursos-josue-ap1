using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCursos.Shared
{
    public class Login
    {

        [Required(ErrorMessage = "* El campo Email es obligatorio")]
        [EmailAddress(ErrorMessage = "* Formato de email incorrecto")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "* El campo password es obligatorio")]
        public string? Password { get; set; }
    }
}
