using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.CuentasPorCobrar
{
    public class cxc_LiquidacionTarjetaDet_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public decimal IdLiquidacion { get; set; }
        public int Secuencia { get; set; }
        public decimal IdMotivo { get; set; }
        public double Porcentaje { get; set; }
        public double Valor { get; set; }

        #region Campos que no existen en la tabla
        public string IdCtaCble { get; set; }
        #endregion

    }
}
