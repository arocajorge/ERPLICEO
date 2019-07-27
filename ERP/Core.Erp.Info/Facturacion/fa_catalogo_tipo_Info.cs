using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Facturacion
{
   public class fa_catalogo_tipo_Info
    {
        public int IdCatalogo_tipo { get; set; }
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = "el campo descripción debe tener máximo 50")]
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
    }
}
