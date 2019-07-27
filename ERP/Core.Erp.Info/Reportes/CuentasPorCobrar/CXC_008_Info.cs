using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.CuentasPorCobrar
{
    public class CXC_008_Info
    {

        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdCbteVta { get; set; }
        public string vt_tipoDoc { get; set; }
        public string vt_NumFactura { get; set; }
        public System.DateTime vt_fecha { get; set; }
        public decimal IdCobro { get; set; }
        public double dc_ValorPago { get; set; }
        public string tc_descripcion { get; set; }
        public System.DateTime cr_fecha { get; set; }
        public string cr_estado { get; set; }
        public string cr_observacion { get; set; }
        public decimal IdCliente { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string Su_Descripcion { get; set; }
        public string IdCobro_tipo { get; set; }
        public Nullable<System.DateTime> FechaLiquidacion { get; set; }
    }
}
