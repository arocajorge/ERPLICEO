using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Facturacion
{
    public class fa_TerminoPago_Info
    {
        public decimal IdTransaccionSession { get; set; }
        [Required(ErrorMessage = ("el campo código es obligatorio"))]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 20")]
        public string IdTerminoPago { get; set; }

        [Required(ErrorMessage = ("el campo descripción es obligatorio"))]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "el campo descripción debe tener mínimo 1 caracter y máximo 50")]
        public string nom_TerminoPago { get; set; }

        [Required(ErrorMessage = ("el campo cuotas es obligatorio"))]
        public int Num_Coutas { get; set; }

        [Required(ErrorMessage = ("el campo dias es obligatorio"))]
        public int Dias_Vct { get; set; }
        public bool estado { get; set; }

        // campos que no existen

        public List<fa_TerminoPago_Distribucion_Info> Lst_fa_TerminoPago_Distribucion { get; set; }
    }
}
