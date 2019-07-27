using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Facturacion
{
    public class fa_formaPago_Info
    {

        [StringLength(2, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter máximo 2")]
        [Required(ErrorMessage = "El campo código es obligatorio")]
        public string IdFormaPago { get; set; }
        [StringLength(500, MinimumLength = 1, ErrorMessage = "el campo descripción debe tener mínimo 1 caracter máximo 500")]
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        public string nom_FormaPago { get; set; }
        public Nullable<bool> Estado { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
    }
}
