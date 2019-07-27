using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.CuentasPorCobrar
{
   public class CXC_005_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdCbteVta { get; set; }
        public string vt_tipoDoc { get; set; }
        public string vt_NumFactura { get; set; }
        public decimal IdCliente { get; set; }
        public string NomCliente { get; set; }
        public System.DateTime vt_fecha { get; set; }
        public System.DateTime vt_fech_venc { get; set; }
        public Nullable<double> Subtotal { get; set; }
        public Nullable<double> IVA { get; set; }
        public Nullable<double> Total { get; set; }
        public double Cobrado { get; set; }
        public double NotaCredito { get; set; }
        public Nullable<double> Saldo { get; set; }
        public string Su_Descripcion { get; set; }
    }
}
