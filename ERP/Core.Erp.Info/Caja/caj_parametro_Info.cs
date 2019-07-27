using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Caja
{
    public class caj_parametro_Info
    {
        public int IdEmpresa { get; set; }

        [Required(ErrorMessage = "El campo ingreso es obligatorio")]
        public int IdTipoCbteCble_MoviCaja_Ing { get; set; }

        [Required(ErrorMessage = "El campo egreso es obligatorio")]
        public int IdTipoCbteCble_MoviCaja_Egr { get; set; }

        [Required(ErrorMessage = "El campo motivo es obligatorio")]
        public Nullable<int> IdTipo_movi_ing_x_reposicion { get; set; }
        public int DiasTransaccionesAFuturo { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> FechaUltMod { get; set; }
    }
}
