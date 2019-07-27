using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Importacion
{
   public class imp_catalogo_Info
    {
        public int IdCatalogo { get; set; }
        [Required(ErrorMessage = ("el campo tipo de catálogo es obligatorio"))]
        public int IdCatalogo_tipo { get; set; }
        [Required(ErrorMessage = ("el campo descripción es obligatorio"))]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "el campo descripción debe tener mínimo 1 caracter y máximo 100")]
        public string ca_descripcion { get; set; }
        public bool estado { get; set; }
    }
}
