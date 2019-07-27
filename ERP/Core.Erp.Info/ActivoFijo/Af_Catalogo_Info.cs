using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.ActivoFijo
{
    public class Af_Catalogo_Info
    {
        public decimal IdTransaccionSession { get; set; }
        [Required(ErrorMessage = ("el campo código es obligatorio"))]
        [StringLength(35, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 35")]

        public string IdCatalogo { get; set; }
        public string IdTipoCatalogo { get; set; }

        [Required(ErrorMessage = ("el campo descripción es obligatorio"))]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "el campo descripción debe tener mínimo 1 caracter y máximo 250")]

        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public string IdUsuario { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> FechaUltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotiAnula { get; set; }
        public bool EstadoBool { get; set; }
    }
}
