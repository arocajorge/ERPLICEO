using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.CuentasPorCobrar
{
    public class CXC_002_diario_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public decimal IdCobro { get; set; }
        public int secuencial { get; set; }
        public string dc_TipoDocumento { get; set; }
        public Nullable<int> IdBodega_Cbte { get; set; }
        public decimal IdCbte_vta_nota { get; set; }
        public int IdEmpresa_ct { get; set; }
        public int IdTipoCbte_ct { get; set; }
        public decimal IdCbteCble_ct { get; set; }
        public string IdCtaCble { get; set; }
        public string pc_Cuenta { get; set; }
        public double dc_Valor { get; set; }
        public double dc_Valor_Debe { get; set; }
        public Nullable<double> dc_Valor_Haber { get; set; }
    }
}
