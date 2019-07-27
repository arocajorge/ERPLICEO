using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.CuentasPorPagar
{
   public class cp_retencion_x_ct_cbtecble_Info
    {
        public int rt_IdEmpresa { get; set; }
        public decimal rt_IdRetencion { get; set; }
        public int ct_IdEmpresa { get; set; }
        public int ct_IdTipoCbte { get; set; }
        public decimal ct_IdCbteCble { get; set; }
        public string Observacion { get; set; }
    }
}
