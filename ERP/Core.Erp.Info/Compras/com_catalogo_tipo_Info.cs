using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Compras
{
    public class com_catalogo_tipo_Info
    {
        [Required(ErrorMessage = ("el campo código es obligatorio"))]
        [StringLength(15, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 15")]
        public string IdCatalogocompra_tipo { get; set; }
        [Required(ErrorMessage = ("el campo descripción es obligatorio"))]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "el campo descripción debe tener mínimo 1 caracter y máximo 50")]
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }

    }
}
