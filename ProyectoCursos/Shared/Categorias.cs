using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCursos.Shared
{
    public class Categorias
    {
        [Key]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "* El campo Nombre de la Categoria es obligatorio")]
        public string? CategoriaNombre { get; set; }

        [Required(ErrorMessage = "* El campo Descripción es obligatorio")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "* El campo Nivel es obligatorio")]
        public int Nivel {get; set; }   
    }
}
