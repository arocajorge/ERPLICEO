using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.CuentasPorCobrar
{
    public class CXC_007_Diario_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public decimal IdLiquidacion { get; set; }
        public string pc_Cuenta { get; set; }
        public int secuencia { get; set; }
        public string IdCtaCble { get; set; }
        public double Debe { get; set; }
        public Nullable<double> Haber { get; set; }
    }
}
