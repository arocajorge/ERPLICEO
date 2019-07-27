using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Facturacion
{
  public  class FAC_001_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdCbteVta { get; set; }
        public int Secuencia { get; set; }
        public decimal IdProducto { get; set; }
        public decimal IdProducto_padre { get; set; }
        public string vt_NumFactura { get; set; }
        public decimal IdCliente { get; set; }
        public int IdVendedor { get; set; }
        public string Ve_Vendedor { get; set; }
        public string NombreCliente { get; set; }
        public string pr_descripcion { get; set; }
        public Nullable<System.DateTime> lote_fecha_vcto { get; set; }
        public string lote_num_lote { get; set; }
        public double vt_cantidad { get; set; }
        public double vt_Precio { get; set; }
        public double vt_PorDescUnitario { get; set; }
        public double vt_DesctTotal { get; set; }
        public double vt_PrecioFinal { get; set; }
        public double vt_Subtotal { get; set; }
        public double vt_iva { get; set; }
        public double vt_total { get; set; }
        public string Estado { get; set; }
        public string Su_Descripcion { get; set; }
        public DateTime vt_fecha { get; set; }
    }
}
