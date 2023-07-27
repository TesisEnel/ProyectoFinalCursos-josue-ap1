using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCursos.Shared
{
    public class Roles
    {
        [Key]
        public int RolId { get; set; } 

        public string? NombreRol { get; set; } 
    }
}
