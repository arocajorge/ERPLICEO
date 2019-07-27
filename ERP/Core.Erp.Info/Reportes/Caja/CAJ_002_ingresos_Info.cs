using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Caja
{
    public class CAJ_002_ingresos_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdConciliacion_Caja { get; set; }
        public int IdTipocbte { get; set; }
        public decimal IdCbteCble { get; set; }
        public Nullable<double> valor_disponible { get; set; }
        public double valor_aplicado { get; set; }
        public double cr_Valor { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string cm_observacion { get; set; }
        public System.DateTime cm_fecha { get; set; }
        public Nullable<decimal> SecuenciaCaja { get; set; }
    }
}
