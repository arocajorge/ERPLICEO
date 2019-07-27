using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.CuentasPorCobrar
{
    public class CXC_009_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdCbteVta { get; set; }
        public string vt_tipoDoc { get; set; }
        public string NumDocumento { get; set; }
        public System.DateTime vt_fecha { get; set; }
        public System.DateTime vt_fech_venc { get; set; }
        public string Su_Descripcion { get; set; }
        public decimal IdCliente { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string pe_cedulaRuc { get; set; }
        public int IdVendedor { get; set; }
        public string Ve_Vendedor { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<int> DiasVencido { get; set; }
        public double TotalCobrado { get; set; }
        public Nullable<double> Saldo { get; set; }
        public string vt_Observacion { get; set; }
        public Nullable<double> X_VENCER { get; set; }
        public Nullable<double> VENCIDO30 { get; set; }
        public Nullable<double> VENCIDO60 { get; set; }
        public Nullable<double> VENCIDO90 { get; set; }
        public Nullable<double> VENCIDO90MAS { get; set; }
    }
}
