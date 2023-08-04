using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCursos.Shared
{
    public class Niveles
    {
        [Key]
        public int NivelId {get;set;}

        public string? Nivelnombre { get;set;}   
    }
}
