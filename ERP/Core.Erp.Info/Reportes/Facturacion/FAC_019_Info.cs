using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Facturacion
{
    public class FAC_019_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdCbteVta { get; set; }
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
        public double ValorRetencion { get; set; }
    }
}
