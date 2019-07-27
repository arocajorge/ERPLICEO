using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.CuentasPorCobrar
{
    public class CXC_003_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdCbteVta { get; set; }
        public string vt_tipoDoc { get; set; }
        public string vt_NumFactura { get; set; }
        public string pe_nombreCompleto { get; set; }
        public decimal IdCliente { get; set; }
        public System.DateTime vt_fecha { get; set; }
        public Nullable<double> ValorRteFTE { get; set; }
        public Nullable<double> ValorRteIVA { get; set; }
        public Nullable<double> PorcentajeRetFTE { get; set; }
        public Nullable<double> PorcentajeRetIVA { get; set; }
        public Nullable<double> TotalRTE { get; set; }
        public Nullable<System.DateTime> cr_fecha { get; set; }
    }
}
