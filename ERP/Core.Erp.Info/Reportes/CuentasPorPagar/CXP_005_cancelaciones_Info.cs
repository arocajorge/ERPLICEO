using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.CuentasPorPagar
{
   public class CXP_005_cancelaciones_Info
    {
        public int IdEmpresa { get; set; }
        public decimal Idcancelacion { get; set; }
        public int Secuencia { get; set; }
        public Nullable<int> IdEmpresa_cxp { get; set; }
        public Nullable<int> IdTipoCbte_cxp { get; set; }
        public Nullable<decimal> IdCbteCble_cxp { get; set; }
        public string Referencia { get; set; }
        public string Observacion { get; set; }
        public double MontoAplicado { get; set; }
        public int IdEmpresa_conciliacion { get; set; }
        public decimal IdConciliacion { get; set; }
    }
}
