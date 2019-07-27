using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.CuentasPorCobrar
{
   public class cxc_Parametro_Info
    {
        public int IdEmpresa { get; set; }
        public int pa_IdCaja_x_cobros_x_CXC { get; set; }
        public int pa_IdTipoMoviCaja_x_Cobros_x_cliente { get; set; }
        public int pa_IdTipoCbteCble_CxC { get; set; }
        public int DiasTransaccionesAFuturo { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> FechaTransac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> FechaUltMod { get; set; }
    }
}
