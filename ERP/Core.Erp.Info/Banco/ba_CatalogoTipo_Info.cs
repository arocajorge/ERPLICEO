using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Banco
{
    public class ba_CatalogoTipo_Info
    {
        [Required(ErrorMessage = ("el campo código es obligatorio"))]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 10")]
        public string IdTipoCatalogo { get; set; }
        [Required(ErrorMessage = ("el campo descripción es obligatorio"))]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "el campo descripción debe tener mínimo 1 caracter y máximo 100")]
        public string tc_Descripcion { get; set; }
    }
}
