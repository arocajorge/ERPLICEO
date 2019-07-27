using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.CuentasPorCobrar
{
    public class CXC_007_Cobros_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public decimal IdLiquidacion { get; set; }
        public int Secuencia { get; set; }
        public double Valor { get; set; }
        public decimal IdCobro { get; set; }
        public System.DateTime cr_fecha { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string cr_observacion { get; set; }
    }
}
