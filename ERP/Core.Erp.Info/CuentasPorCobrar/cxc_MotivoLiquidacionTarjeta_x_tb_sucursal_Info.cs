using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.CuentasPorCobrar
{
    public class cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdMotivo { get; set; }
        public int Secuencia { get; set; }
        public int IdSucursal { get; set; }
        public string IdCtaCble { get; set; }
        public string pc_Cuenta { get; set; }
        public string Su_Descripcion { get; set; }
    }
}
